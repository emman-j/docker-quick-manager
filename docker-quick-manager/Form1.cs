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
            dataGridView1.DataSource = _dockerManager.ContainersBindingSource;
            dataGridView2.DataSource = _dockerManager.ContainersBindingSource;

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
                await _dockerManager.StartContainer(_dockerManager.SelectedContainer.Id);
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow?.DataBoundItem is DockerContainer container)
            {
                _dockerManager.SelectedContainer = container;
            }
        }

        private void CreateButtton_Click(object sender, EventArgs e)
        {

        }
    }
}
