using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class SettingsViewModel
    {
        public ICommand LicensesCommand { get; set; }


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
            LicensesCommand = new Command<string>(async (string licenseFile) =>
            {                
                await navigation.PushAsync(new LicensesPage(licenseFile));
            });
        }

    }
}
