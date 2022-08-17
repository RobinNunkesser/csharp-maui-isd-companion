using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Essentials;
using System.Threading.Tasks;
using AndroidX.Core.View;
using ISDCompanion.Helpers;
using Xamarin.Forms;
using Android.Views;

[assembly: Dependency(typeof(ISDCompanion.Droid.Environment))]

namespace ISDCompanion.Droid
{
    [Activity(Label = "ISDCompanion", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App());

            //toBeDeleted
            //Window.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LightNavigationBar;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class Environment : IEnvironment
    {
        public async void SetStatusBarColor(bool darkMode)
        {
            if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Lollipop)
                return;

            var activity = Platform.CurrentActivity;
            var window = activity.Window;

            await Task.Delay(50);

            if (darkMode)
            {
                WindowCompat.GetInsetsController(window, window.DecorView).AppearanceLightStatusBars = false;
                window.SetStatusBarColor(Android.Graphics.Color.Black);
            }
            else
            {
                WindowCompat.GetInsetsController(window, window.DecorView).AppearanceLightStatusBars = true;
                window.SetStatusBarColor(Android.Graphics.Color.White);
            }
        }

        public async void SetNavigationBarColor(bool darkMode)
        {
            if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Lollipop)
                return;

            var activity = Platform.CurrentActivity;
            var window = activity.Window;

            await Task.Delay(50);

            if (darkMode)
            {
                window.SetNavigationBarColor(Android.Graphics.Color.Black);
            }
            else
            {
                window.SetNavigationBarColor(Android.Graphics.Color.White);
                window.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LightNavigationBar;
            }
        }
    }
}
