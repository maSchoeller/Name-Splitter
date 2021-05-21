using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using NameSplitter.Core;
using NameSplitter.Core.Models;

namespace NameSplitter.View.ViewModels
{
    public class NameViewModel : BindableBase
    {
        private readonly IServiceProvider _provider;
        public NameViewModel(IServiceProvider provider)
        {
            SaveCommand = new RelayCommand(OnSave, CanSave);
            DeleteCommand = new RelayCommand(OnDelete, CanDelete);
            _provider = provider;
            _saveError = string.Empty;
            _ = LoadDataAsync();
        }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        private string? _name = string.Empty;
        public string? Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        public ObservableCollection<KeyDisplayPair> Names { get; }
                        = new ObservableCollection<KeyDisplayPair>();

        private string? _selectedNameKey;
        public string? SelectedNameKey { get => _selectedNameKey; set => Set(ref _selectedNameKey, value); }

        private string _saveError;

        public string SaveError
        {
            get => _saveError;
            set => Set(ref _saveError, value);
        }

        private async void OnSave(object? o)
        {
            var context = _provider.GetRequiredService<NameSplitterDBContext>();
            if (context.NameFillingWords.Any(w => w.FillingWord == Name))
            {
                SaveError = "Der Datensatz ist bereits vorhanden.";
            }
            else
            {
                SaveError = string.Empty;
                var name = new NameFillingWord()
                {
                    FillingWord = Name!
                };
                Name = null;
                context.NameFillingWords.Add(name);
                await context.SaveChangesAsync();
                await LoadDataAsync();
            }
        }

        private bool CanSave(object? o)
            => !string.IsNullOrWhiteSpace(Name);

        private async void OnDelete(object? o)
        {
            var context = _provider.GetRequiredService<NameSplitterDBContext>();
            var result = await context.NameFillingWords.FindAsync(SelectedNameKey);
            if (result is not null)
            {
                context.NameFillingWords.Remove(result);
                await context.SaveChangesAsync();
                await LoadDataAsync();
            }
        }
        private bool CanDelete(object? o)
            => _selectedNameKey is not null
                && Names.Count > 0;

        private async Task LoadDataAsync()
        {
            Names.Clear();
            var context = _provider.GetRequiredService<NameSplitterDBContext>();
            var namesList = await context.NameFillingWords.ToListAsync();
            namesList.ForEach(n => Names.Add(new KeyDisplayPair(n.Id, n.FillingWord)));
        }

    }
}
