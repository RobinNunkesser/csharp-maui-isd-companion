using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ISDCompanion
{
    public partial class ShortestPathsPage : ContentPage
    {
        public ShortestPathsPage()
        {
            InitializeComponent();
            BindingContext = new ShortestPathsViewModel();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Switch.IsToggled = false;
        }
    }
}
