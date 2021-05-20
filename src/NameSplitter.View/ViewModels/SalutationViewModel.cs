using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using NameSplitter.Core.Models;

namespace NameSplitter.View.ViewModels
{
    public class SalutationViewModel : BindableBase
    {
        public SalutationViewModel()
        {
            SaveCommand = new RelayCommand(OnSave, CanSave);
            DeleteCommand = new RelayCommand(OnDelete);
        }

        private string _salutation = string.Empty;
        public string Salutation { get => _salutation; set => Set(ref _salutation, value); }

        public Gender[] Genders => Enum.GetValues(typeof(Gender)) as Gender[] ?? Array.Empty<Gender>();
        private Gender _gender;
        public Gender SelectedGender { get => _gender; set => Set(ref _gender, value); }


        public Language[] Languages => Enum.GetValues(typeof(Language)) as Language[] ?? Array.Empty<Language>();
        private Language _language;
        public Language SelectedLanguage { get => _language; set => Set(ref _language, value); }


        public SalutationForm[] Forms => Enum.GetValues(typeof(SalutationForm)) as SalutationForm[] ?? Array.Empty<SalutationForm>();
        private SalutationForm _form;
        public SalutationForm SelectedForm { get => _form; set => Set(ref _form, value); }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        private void OnSave(object? o)
        {
            var salutation = new Salutation
            {
                Content = Salutation,
                Form = SelectedForm,
                Gender = SelectedGender,
                Language = SelectedLanguage
            };

            //TODO: save in DbContext
        }

        private bool CanSave(object? arg) 
            => !string.IsNullOrWhiteSpace(Salutation);

        private void OnDelete(object? obj)
        {
        }
    }
}
