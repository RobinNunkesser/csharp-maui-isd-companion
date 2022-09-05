using System;
using ISDCompanion.Helpers;
using ISDCompanion.Resx;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ISDCompanion
{
    public partial class App : Application
    {
        public App()
        {
            LocalizationResourceManager.Current.PropertyChanged += (sender, e) => AppResources.Culture = LocalizationResourceManager.Current.CurrentCulture;
            LocalizationResourceManager.Current.Init(AppResources.ResourceManager);

            InitializeComponent();
            InitializeMainPage();
        }

        private void InitializeMainPage()
        {
            if (Settings.WelcomeStatus == (int)Enums.WelcomeStatusType.Finished)
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new StatusPage();
            }
        }

        protected override void OnStart()
        {
            RequestedThemeChanged += App_RequestedThemeChanged;
            TheTheme.SetTheme();
        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }

        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TheTheme.SetTheme();
            });
        }
    }
}
