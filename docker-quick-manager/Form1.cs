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
            SelectedContainerTextbox.DataBindings.Add("Text", _dockerManager.SelectedContainer, nameof(DockerContainer.Name), true, DataSourceUpdateMode.OnPropertyChanged);
            ARSelectedContainterTextBox.DataBindings.Add("Text", _dockerManager.SelectedContainer, nameof(DockerContainer.Name), true, DataSourceUpdateMode.OnPropertyChanged);
            await _dockerManager.GetContainersAsync();
            dataGridView1.DataSource = _dockerManager.ContainersBindingSource;
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
                await _dockerManager.StartContainer(_dockerManager.SelectedContainer.Id);
            }
        }
    }
}
