using System.Reflection;

namespace StudyCompanion
{
    public partial class LicensesPage : ContentPage
    {
        private string licenseFile { get; set; }

        public LicensesPage(string licenseFile)
        {
            InitializeComponent();
            this.licenseFile = licenseFile;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            using var stream = await FileSystem.OpenAppPackageFileAsync(licenseFile);
            using var reader = new StreamReader(stream);

            var html = reader.ReadToEnd();

            var htmlSource = new HtmlWebViewSource
            {
                Html = html
            };
            Browser.Source = htmlSource;
        }

    }
}
