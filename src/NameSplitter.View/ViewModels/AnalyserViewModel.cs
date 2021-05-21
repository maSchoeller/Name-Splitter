using System;
using System.Collections.ObjectModel;
using System.Globalization;
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
            ResultCollection = new ObservableCollection<ResultSet>();
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
            var generator = _provider.GetRequiredService<SalutationGenerator>();
            var result = await analyser.AnalyseAsync(Input!);
            var con = new EnumValuteConverter();
            ResultCollection.Clear();
            ResultCollection.Add(new ResultSet("Erfolgreiches Splitten:", result.IsValid.ToString(CultureInfo.CurrentCulture)));
            if (result.Salutation is not null)
            {
                ResultCollection.Add(new ResultSet("Erkanntes Geschlecht:", con.ConvertTyped(result.Salutation.Gender)));
                ResultCollection.Add(new ResultSet("Erkannte Sprache:", con.ConvertTyped(result.Salutation.Language)));
            }

            ResultCollection.Add(new ResultSet("Vorname:", result.Firstname));
            ResultCollection.Add(new ResultSet("Nachname:", result.Lastname));
            foreach (var title in result.Titles)
            {
                ResultCollection.Add(new ResultSet("Erkannter Titel:", title.Content));
            }

            Output = generator.GenerateSalutation(result);
        }

        private bool CanAnalyse(object? o)
            => !string.IsNullOrWhiteSpace(Input);

        public ObservableCollection<ResultSet> ResultCollection { get; }
    }

    public record ResultSet(string key, string value);
}
