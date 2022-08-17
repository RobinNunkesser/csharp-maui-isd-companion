using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ISDCompanion.Helpers
{
    public static class TheTheme
    {
        public static void SetTheme()
        {
            switch (Settings.Theme)
            {
                //default
                case 0:
                    App.Current.UserAppTheme = OSAppTheme.Unspecified;
                    break;
                //light
                case 1:
                    App.Current.UserAppTheme = OSAppTheme.Light;
                    break;
                //dark
                case 2:
                    App.Current.UserAppTheme = OSAppTheme.Dark;
                    break;
            }

            var environment = DependencyService.Get<IEnvironment>();
            if (App.Current.RequestedTheme == OSAppTheme.Dark)
            {
                environment?.SetStatusBarColor(true);
                environment?.SetNavigationBarColor(true);
            }
            else
            {
                environment?.SetStatusBarColor(false);
                environment?.SetNavigationBarColor(false);
            }
        }
    }
}
