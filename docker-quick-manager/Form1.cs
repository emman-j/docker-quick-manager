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
            _dockerManager.OnError += (s, e) =>
            {
                MessageBox.Show($"Ensure docker engine is running.\n\nDocker Error: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

        }

        private async void Setup_Form_Shown(object sender, EventArgs e)
        {
            if (await _dockerManager.IsDockerEngineRunning())
            {
                await _dockerManager.GetContainersAsync();
                await _dockerManager.GetImagesAsync();
            }
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
            if (_dockerManager.SelectedContainer != null)
            {
                // Open PowerShell console and run the docker command
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"docker start -ai {_dockerManager.SelectedContainerName}",
                    UseShellExecute = true
                });
                _dockerManager.SelectedContainer.IsRunning = true;
            }
            else
            {
                MessageBox.Show("Please select a container to start.", "No Container Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
                await _dockerManager.CreateContainer(imageName, containerName, targetDir);
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
                await _dockerManager.RemoveContainer(_dockerManager.SelectedContainer);
            }
            else
            {
                MessageBox.Show("Please select a container to remove.", "No Container Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void RefreshButton_Click(object sender, EventArgs e)
        {
            await _dockerManager.GetContainersAsync();
            await _dockerManager.GetImagesAsync();
        }
    }
}
