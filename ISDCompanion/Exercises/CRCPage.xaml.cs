using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ISDCompanion
{
    public partial class CRCPage : ContentPage
    {
        public CRCPage()
        {
            InitializeComponent();
            BindingContext = new CRCViewModel();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Switch.IsToggled = false;
        }
    }
}
