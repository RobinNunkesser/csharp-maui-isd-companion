using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ISDCompanion
{
    public partial class MSTPage : ContentPage
    {
        public MSTPage()
        {
            InitializeComponent();
            BindingContext = new MSTViewModel();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Switch.IsToggled = false;
        }
    }
}
