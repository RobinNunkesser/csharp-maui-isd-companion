using System;
using System.Collections.Generic;
using ISDCompanion.Resx;
using Xamarin.Forms;

namespace ISDCompanion
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = new SettingsViewModel(Navigation);
        }
    }
}
