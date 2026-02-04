using Docker.DotNet;
using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace docker_quick_manager
{

    public class DockerManager : INotifyPropertyChanged
    {
        BindingList<DockerContainer> _containers = new BindingList<DockerContainer>();
        List<string> _images = new List<string>();
        private readonly Uri _uri;
        private DockerContainer? _selectedContainer;
        private BindingSource? _containersBindingSource;
        private BindingSource? _imagesBindingSource;
        private bool _isDockerRunning = false;

        public DockerClientConfiguration DockerClient { get; set; }
        public event EventHandler<Exception> OnError;

        public DockerContainer? SelectedContainer
        {
            get => _selectedContainer;
            set
            {
                SetValue(ref _selectedContainer, value);
                NotifyPropertyChanged(nameof(SelectedContainerName));
            }
        }
        public string SelectedContainerName => SelectedContainer?.Name ?? "";
        public BindingList<DockerContainer> Containers => _containers;
        public BindingSource ContainersBindingSource
        {
            get
            {
                if (_containersBindingSource == null)
                {
                    _containersBindingSource = new BindingSource() { DataSource = _containers };
                }
                return _containersBindingSource;
            }
        }
        public BindingSource ImagesBindingSource
        {
            get
            {
                if (_imagesBindingSource == null)
                {
                    _imagesBindingSource = new BindingSource() { DataSource = _images };
                }
                return _imagesBindingSource;
            }
        }
        public bool IsDockerRunning { get => _isDockerRunning; set => SetValue(ref _isDockerRunning, value); }

        public event PropertyChangedEventHandler? PropertyChanged;

        public DockerManager(Uri uri = null)
        {
            _uri = uri ?? new Uri("npipe://./pipe/docker_engine");
            try
            {
                DockerClient = new DockerClientConfiguration(_uri);
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, ex);
            }
        }

        private void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        private void SetValue<T>(ref T field, T value, [CallerMemberName]string propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                NotifyPropertyChanged(propertyName);
            }
        }

        public async Task<bool> IsDockerEngineRunning()
        {
            IsDockerRunning = false;
            try
            {
                using (var client = DockerClient.CreateClient())
                {
                    // Try to ping the Docker daemon
                    var version = await client.System.GetVersionAsync();
                    IsDockerRunning = version != null;
                    return IsDockerRunning;
                }
            }
            catch
            {
                return IsDockerRunning;
            }
        }
        public async Task<BindingList<DockerContainer>> GetContainersAsync()
        {
            try
            {
                if(!IsDockerRunning)
                    return _containers;

                string previouslySelectedId = _selectedContainer?.Id;
                _containers.Clear(); // Clear previous containers
                using (var client = DockerClient.CreateClient())
                {
                    var containers = await client.Containers.ListContainersAsync(
                        new ContainersListParameters()
                        {
                            Limit = 10,
                        });

                    foreach (var container in containers)
                    {
                        _containers.Add(new DockerContainer()
                        {
                            Id = container.ID,
                            Names = container.Names.ToList(),
                            IsRunning = container.State == "running"
                        });
                    }
                }

                // Notify that the containers list has changed
                NotifyPropertyChanged(nameof(Containers));
                if (_containersBindingSource != null)
                {
                    _containersBindingSource.ResetBindings(false);
                }

                return _containers;
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, ex);
                return _containers;
            }
        }

        public async Task<List<string>> GetImagesAsync()
        {
            try
            {
                if (!IsDockerRunning)
                    return new List<string>();

                _images.Clear(); // Clear previous images
                using (var client = DockerClient.CreateClient())
                {
                    var images = await client.Images.ListImagesAsync(new ImagesListParameters());

                    foreach (var image in images)
                    {
                        if (image.RepoTags != null && image.RepoTags.Count > 0)
                        {
                            _images.AddRange(image.RepoTags.Where(tag => !string.IsNullOrEmpty(tag)).ToList());
                        }
                    }
                }
                // Notify that the images list has changed
                if (_imagesBindingSource != null)
                {
                    _imagesBindingSource.ResetBindings(false);
                }
                return _images;
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, ex);
                return _images;
            }
        }
        public async Task StartContainer(string containerId)
        {
            try
            {
                if (!IsDockerRunning)
                    return;

                using (var client = DockerClient.CreateClient())
                {
                    await client.Containers.StartContainerAsync(containerId, new ContainerStartParameters());
                }
                await GetContainersAsync();
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, ex);
            }
        }
        public async Task StopContainer(DockerContainer container)
        {
            try
            {
                if (!IsDockerRunning)
                    return;

                if (container != null)
                {
                    await StopContainer(container.Id);
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, ex);
            }
        }
        public async Task StopContainer(string containerId)
        {
            try
            {
                if (!IsDockerRunning)
                    return;

                using (var client = DockerClient.CreateClient())
                {
                    await client.Containers.StopContainerAsync(containerId, new ContainerStopParameters());
                }
                await GetContainersAsync();
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, ex);
            }
        }

        public async Task<string> CreateContainer(string imageName, string containerName, string targetDir)
        {
            try
            {
                if (!IsDockerRunning)
                    return string.Empty;

                using (var client = DockerClient.CreateClient())
                {
                    var createParams = new CreateContainerParameters()
                    {
                        Name = containerName,
                        Image = imageName,
                        WorkingDir = "/app",
                        Cmd = new List<string> { "sh" },
                        AttachStdin = true,
                        AttachStdout = true,
                        AttachStderr = true,
                        Tty = true, // Allocate a pseudo-TTY
                        OpenStdin = true, // Keep STDIN open even if not attached
                        HostConfig = new HostConfig()
                        {
                            Binds = new List<string> { $"{targetDir}:/app:rw" } // Mount volume
                        }
                    };

                    var response = await client.Containers.CreateContainerAsync(createParams);
                    return response.ID; // Return the container ID
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, ex);
                throw; // Re-throw to let caller handle the error
            }
            finally
            {
                await GetContainersAsync();
            }
        }
        public async Task RemoveContainer(DockerContainer container)
        {
            try
            {
                if (!IsDockerRunning)
                    return;

                // Remove the container using Docker API
                using (var client = DockerClient.CreateClient())
                {
                    await client.Containers.RemoveContainerAsync(container.Id,
                        new ContainerRemoveParameters()
                        {
                            Force = true // Force removal even if container is running
                        });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing container: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                await GetContainersAsync();
            }
        }
    }
}
