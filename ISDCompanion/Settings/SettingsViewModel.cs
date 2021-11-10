using System;
using System.Collections.Generic;
using System.Windows.Input;
using ISDCompanion.Resx;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class SettingsViewModel : ViewModel
    {
        public ICommand LicensesCommand { get; set; }
        public ICommand NavigateCommand { get; set; }

        public int Status
        {
            get => Settings.Status;
            set => Settings.Status = value;
        }

        public int Semester {
            get => Settings.Semester;
            set => Settings.Semester = value;
        }

        public int Specialization
        {
            get => Settings.Specialization;
            set => Settings.Specialization = value;
        }

        public SettingsViewModel(INavigation navigation)
        {
            OnPropertyChanged();
            LicensesCommand = new Command<string>(async (string licenseFile) =>
            {                
                await navigation.PushAsync(new LicensesPage(licenseFile));
            });
            NavigateCommand = new Command<Type>(async (Type pageType) =>
            {
                Page page = (Page)Activator.CreateInstance(pageType);
                await navigation.PushAsync(page);
            });
        }

    }
}
