using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class SettingsViewModel
    {
        public ICommand LicensesCommand { get; set; }

        public SettingsViewModel(INavigation navigation)
        {
            LicensesCommand = new Command(async () =>
            {                
                await navigation.PushAsync(new LicensesPage());
            });
        }

    }
}
