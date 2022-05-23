using System;
using System.Collections.Generic;
using DevExpress.XamarinForms.Core.Themes;
using Xamarin.Forms;

namespace ISDCompanion
{
    public partial class SchedulePage : ContentPage
    {
        public SchedulePage()
        {
            var currentTheme = Application.Current.RequestedTheme;
            switch (currentTheme)
            {
                case OSAppTheme.Light: ThemeManager.ThemeName = Theme.Light; break;
                case OSAppTheme.Dark: ThemeManager.ThemeName = Theme.Dark; break;
            }            
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new ScheduleViewModel();
        }
    }
}
