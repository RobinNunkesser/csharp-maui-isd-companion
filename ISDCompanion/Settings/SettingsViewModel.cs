using System.Windows.Input;

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
