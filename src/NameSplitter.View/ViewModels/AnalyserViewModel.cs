using System;
using System.Windows.Input;

using Microsoft.Extensions.DependencyInjection;

using NameSplitter.Core;

namespace NameSplitter.View.ViewModels
{
    public class AnalyserViewModel : BindableBase
    {
        private readonly IServiceProvider _provider;
        public AnalyserViewModel(IServiceProvider provider)
        {
            AnalyseCommand = new RelayCommand(OnAnalyseAsync, CanAnalyse);
            _provider = provider;
        }
        public ICommand AnalyseCommand { get; }

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

        private async void OnAnalyseAsync(object? o)
        {
            var analyser = _provider.GetRequiredService<NameSplitterAnalyser>();
            var result = await analyser.AnalyseAsync(Input!);
        }

        private bool CanAnalyse(object? o) 
            => !string.IsNullOrWhiteSpace(Input);
    }
}
