using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ISDCompanion
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        async void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new LicensesPage());
        }
    }
}
