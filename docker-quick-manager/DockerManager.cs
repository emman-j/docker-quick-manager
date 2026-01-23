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
        List<IImageOperations> _images = new List<IImageOperations>();
        private readonly Uri _uri;
        private DockerContainer? _selectedContainer;
        private BindingSource? _containersBindingSource;

        public DockerClientConfiguration DockerClient { get; set; }
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

        public event PropertyChangedEventHandler? PropertyChanged;

        public DockerManager(Uri uri = null)
        {
            _uri = uri ?? new Uri("npipe://./pipe/docker_engine");
            DockerClient = new DockerClientConfiguration(_uri);
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

        public async Task<BindingList<DockerContainer>> GetContainersAsync()
        {
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

        public async Task<List<ImagesListResponse>> GetImagesAsync()
        {
            using (var client = DockerClient.CreateClient())
            {
                var images = await client.Images.ListImagesAsync(new ImagesListParameters());
                return images.ToList();
            }
        }
        public async Task StartContainer(string containerId)
        {
            using (var client = DockerClient.CreateClient())
            {
                await client.Containers.StartContainerAsync(containerId, new ContainerStartParameters());
            }
            await GetContainersAsync();
        }
        public async Task StopContainer(DockerContainer container)
        {
            if (container != null)
            {
                await StopContainer(container.Id);
            }
        }
        public async Task StopContainer(string containerId)
        {
            using (var client = DockerClient.CreateClient())
            {
                await client.Containers.StopContainerAsync(containerId, new ContainerStopParameters());
            }
            await GetContainersAsync();
        }
    }
}
