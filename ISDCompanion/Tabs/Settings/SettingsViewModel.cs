using System.Windows.Input;

namespace StudyCompanion
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
                using var stream = await FileSystem.OpenAppPackageFileAsync(licenseFile);
                using var reader = new StreamReader(stream);
                var html = reader.ReadToEnd();
                var htmlSource = new HtmlWebViewSource
                {
                    Html = html
                };
                await navigation.PushAsync(new InternalBrowserPage(htmlSource));
            });
            NavigateCommand = new Command<Type>(async (Type pageType) =>
            {
                Page page = (Page)Activator.CreateInstance(pageType);
                await navigation.PushAsync(page);
            });
        }

    }
}
