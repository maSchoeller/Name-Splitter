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
    public class TitleViewModel : BindableBase
    {
        private readonly IServiceProvider _provider;
        public TitleViewModel(IServiceProvider provider)
        {
            SaveCommand = new RelayCommand(OnSave, CanSave);
            DeleteCommand = new RelayCommand(OnDelete, CanDelete);
            _provider = provider;
            _saveError = string.Empty;
            _ = LoadDataAsync();
        }
        private string? _title = string.Empty;
        public string? Title { get => _title; set => Set(ref _title, value); }

        public AcademicDegree[] AcademicDegrees => Enum.GetValues(typeof(AcademicDegree)) as AcademicDegree[] ?? Array.Empty<AcademicDegree>();
        private AcademicDegree? _academicDegree;
        public AcademicDegree? SelectedAcademicDegree { get => _academicDegree; set => Set(ref _academicDegree, value); }


        public Language[] Languages => Enum.GetValues(typeof(Language)) as Language[] ?? Array.Empty<Language>();
        private Language? _language;
        public Language? SelectedLanguage { get => _language; set => Set(ref _language, value); }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public ObservableCollection<KeyDisplayPair> Titles { get; }
            = new ObservableCollection<KeyDisplayPair>();

        private string? _selectedtitleKey;
        public string? SelectedTitleKey { get => _selectedtitleKey; set => Set(ref _selectedtitleKey, value); }

        private string _saveError;

        public string SaveError
        {
            get => _saveError;
            set => Set(ref _saveError, value);
        }

        private async void OnSave(object? o)
        {
            var context = _provider.GetRequiredService<NameSplitterDBContext>();
            if (context.Titles.Any(s => s.Content == Title))
            {
                SaveError = "Der Datensatz ist bereits vorhanden.";
            }
            else
            {
                SaveError = string.Empty;
                var titles = new Title
                {
                    Content = Title!,
                    Degree = SelectedAcademicDegree!.Value,
                    Language = SelectedLanguage!.Value
                };
                context.Titles.Add(titles);
                await context.SaveChangesAsync();
                Title = string.Empty;
                SelectedAcademicDegree = null;
                SelectedLanguage = null;
                await LoadDataAsync();
            }
        }

        private bool CanSave(object? o)
            => !string.IsNullOrWhiteSpace(Title)
            && SelectedAcademicDegree is not null
            && SelectedLanguage is not null;

        private async void OnDelete(object? o)
        {
            var context = _provider.GetRequiredService<NameSplitterDBContext>();
            var result = await context.Titles.FindAsync(SelectedTitleKey);
            if (result is not null)
            {
                context.Titles.Remove(result);
                await context.SaveChangesAsync();
                await LoadDataAsync();
            }
        }
        private bool CanDelete(object? o)
            => SelectedTitleKey is not null
                && Titles.Count > 0;

        private async Task LoadDataAsync()
        {
            Titles.Clear();
            var context = _provider.GetRequiredService<NameSplitterDBContext>();
            var namesList = await context.Titles.ToListAsync();
            namesList.ForEach(n => Titles.Add(new KeyDisplayPair(n.Id, n.Content)));
        }
    }
}
