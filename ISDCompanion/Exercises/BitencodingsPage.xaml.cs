using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ISDCompanion
{
    public partial class BitencodingsPage : ContentPage
    {
        public BitencodingsPage()
        {
            InitializeComponent();
            BindingContext = new BitencodingsViewModel();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            SwitchNRZ.IsToggled = false;
            SwitchNRZI.IsToggled = false;
            SwitchMLT3.IsToggled = false;
        }
    }
}
