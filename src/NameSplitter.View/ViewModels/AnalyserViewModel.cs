using System.Windows.Input;

namespace NameSplitter.View.ViewModels
{
    public class AnalyserViewModel : BindableBase
    {
        public AnalyserViewModel()
        {
            SaveCommand = new RelayCommand(OnSave, CanSave);
            DeleteCommand = new RelayCommand(OnDelete, CanDelete);
        }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        private string? _input;
        public string? Input
        {
            get => _input;
            set => Set(ref _input, value);
        }


        private string? _output;
        public string? Output
        {
            get => _output;
            set => Set(ref _output, value);
        }

        private async void OnSave(object? o)
        {

        }

        private bool CanSave(object? o)
        {
            return false;
        }

        private async void OnDelete(object? o)
        {

        }
        private bool CanDelete(object? o)
        {
            return false;
        }

    }
}
