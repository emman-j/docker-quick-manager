using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace docker_quick_manager
{
    public class DockerContainer : INotifyPropertyChanged
    {
        private string _id;
        private List<string> _names = new List<string>();
        private bool _isRunning;

        public string Id { get => _id; set => SetValue(ref _id, value); }
        public string Name => string.Join(",", _names).Replace("/", "");
        public List<string> Names { get => _names; set => SetValue(ref _names, value); }
        [ReadOnly(true)]
        public bool IsRunning { get => _isRunning; set => SetValue(ref _isRunning, value); }
        public event PropertyChangedEventHandler? PropertyChanged;

        public override string ToString() => $"{Name} ({Id})";
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        private void SetValue<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                NotifyPropertyChanged(propertyName);
            }
        }
    }
}
