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
    public class SalutationViewModel : BindableBase
    {
        private readonly IServiceProvider _provider;
        public SalutationViewModel(IServiceProvider provider)
        {
            SaveCommand = new RelayCommand(OnSave, CanSave);
            DeleteCommand = new RelayCommand(OnDelete, CanDelete);
            _provider = provider;
            _saveError = string.Empty;
            _ = LoadDataAsync();
        }


        private string _salutation = string.Empty;
        public string Salutation { get => _salutation; set => Set(ref _salutation, value); }

        public Gender[] Genders => Enum.GetValues(typeof(Gender)) as Gender[] ?? Array.Empty<Gender>();
        private Gender? _gender;
        public Gender? SelectedGender { get => _gender; set => Set(ref _gender, value); }


        public Language[] Languages => Enum.GetValues(typeof(Language)) as Language[] ?? Array.Empty<Language>();
        private Language? _language;
        public Language? SelectedLanguage { get => _language; set => Set(ref _language, value); }


        public SalutationForm[] Forms => Enum.GetValues(typeof(SalutationForm)) as SalutationForm[] ?? Array.Empty<SalutationForm>();
        private SalutationForm? _form;
        public SalutationForm? SelectedForm { get => _form; set => Set(ref _form, value); }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public ObservableCollection<KeyDisplayPair> Salutations { get; }
            = new ObservableCollection<KeyDisplayPair>();

        private string? _selectedSalutationKey;
        public string? SelectedSalutationKey { get => _selectedSalutationKey; set => Set(ref _selectedSalutationKey, value); }

        private string _saveError;

        public string SaveError
        {
            get => _saveError;
            set => Set(ref _saveError, value);
        }

        private async void OnSave(object? o)
        {
            var context = _provider.GetRequiredService<NameSplitterDBContext>();
            if (context.Salutations.Any(s => s.Content == Salutation))
            {
                SaveError = "Der Datensatz ist bereits vorhanden.";
            }
            else
            {
                SaveError = string.Empty;
                var salutation = new Salutation
                {
                    Content = Salutation!,
                    Gender = SelectedGender!.Value,
                    Language = SelectedLanguage!.Value,
                    Form = SelectedForm!.Value,
                };
                context.Salutations.Add(salutation);
                await context.SaveChangesAsync();
                Salutation = string.Empty;
                SelectedGender = null;
                SelectedLanguage = null;
                SelectedForm = null;
                await LoadDataAsync();
            }
        }

        private bool CanSave(object? o)
            => !string.IsNullOrWhiteSpace(Salutation)
            && SelectedForm is not null
            && SelectedGender is not null
            && SelectedLanguage is not null;

        private async void OnDelete(object? o)
        {
            var context = _provider.GetRequiredService<NameSplitterDBContext>();
            var result = await context.Salutations.FindAsync(SelectedSalutationKey);
            if (result is not null)
            {
                context.Salutations.Remove(result);
                await context.SaveChangesAsync();
                await LoadDataAsync();
            }
        }

        private bool CanDelete(object? o)
            => SelectedSalutationKey is not null
                && Salutations.Count > 0;

        private async Task LoadDataAsync()
        {
            Salutations.Clear();
            var context = _provider.GetRequiredService<NameSplitterDBContext>();
            var namesList = await context.Salutations.ToListAsync();
            namesList.ForEach(n => Salutations.Add(new KeyDisplayPair(n.Id, n.Content)));
        }
    }
}
