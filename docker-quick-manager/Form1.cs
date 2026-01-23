using System;
using System.ComponentModel;

namespace docker_quick_manager
{
    public partial class Setup_Form : Form
    {
        private readonly DockerManager _dockerManager;
        public Setup_Form()
        {
            InitializeComponent();

            _dockerManager = new DockerManager(new Uri("npipe://./pipe/docker_engine"));

        }

        private async void Setup_Form_Shown(object sender, EventArgs e)
        {
            await _dockerManager.GetContainersAsync();
            await _dockerManager.GetImagesAsync();
            dataGridView1.DataSource = _dockerManager.ContainersBindingSource;
            dataGridView2.DataSource = _dockerManager.ContainersBindingSource;
            ImageComboBox.DataSource = _dockerManager.ImagesBindingSource;

            SelectedContainerTextbox.DataBindings.Add("Text", _dockerManager, nameof(DockerManager.SelectedContainerName), true, DataSourceUpdateMode.OnPropertyChanged);
            ARSelectedContainterTextBox.DataBindings.Add("Text", _dockerManager, nameof(DockerManager.SelectedContainerName), true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private async void StopButton_Click(object sender, EventArgs e)
        {
            if (_dockerManager.SelectedContainer != null)
            {
                await _dockerManager.StopContainer(_dockerManager.SelectedContainer);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow?.DataBoundItem is DockerContainer container)
            {
                _dockerManager.SelectedContainer = container;
            }
        }
        private async void StartButton_Click(object sender, EventArgs e)
        {
            string containerId = string.Empty;

            if (_dockerManager.SelectedContainer != null)
                containerId = _dockerManager.SelectedContainer.Id;

            if (string.IsNullOrEmpty(containerId))
                throw new InvalidOperationException("No container selected.");

            // Start the container first
            await _dockerManager.StartContainer(containerId);

            // Open PowerShell console and run the docker command
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"-Command \"docker attach {containerId}\"",
                UseShellExecute = true
            });
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow?.DataBoundItem is DockerContainer container)
            {
                _dockerManager.SelectedContainer = container;
            }
        }

        private async void CreateButtton_Click(object sender, EventArgs e)
        {
            string imageName = ImageComboBox.Text;
            string containerName = NameTextBox.Text;
            string targetDir = TragetDirTextBox.Text;

            if (string.IsNullOrWhiteSpace(imageName))
            {
                MessageBox.Show("Please select an image.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(containerName))
            {
                MessageBox.Show("Please enter a container name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(targetDir))
            {
                MessageBox.Show("Please select a target directory.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Run the docker command to create the container (without starting it interactively)
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-Command \"docker create -v '{targetDir}:/app' -w /app --name '{containerName}' '{imageName}' sh\"",
                    UseShellExecute = true
                });

                await _dockerManager.GetContainersAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating container: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FolderSelectButton_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select Target Directory";
                folderDialog.UseDescriptionForTitle = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    TragetDirTextBox.Text = folderDialog.SelectedPath;
                }
            }
        }

        private async void RemoveButton_Click(object sender, EventArgs e)
        {
            if (_dockerManager.SelectedContainer != null)
            {
                try
                {
                    // Remove the container using Docker API
                    using (var client = _dockerManager.DockerClient.CreateClient())
                    {
                        await client.Containers.RemoveContainerAsync(_dockerManager.SelectedContainer.Id,
                            new Docker.DotNet.Models.ContainerRemoveParameters()
                            {
                                Force = true // Force removal even if container is running
                            });
                    }

                    // Refresh the container list after removal
                    await _dockerManager.GetContainersAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error removing container: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a container to remove.", "No Container Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
