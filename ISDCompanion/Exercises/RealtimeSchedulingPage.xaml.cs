using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ISDCompanion
{
    public partial class RealtimeSchedulingPage : ContentPage
    {
        public RealtimeSchedulingPage()
        {
            InitializeComponent();
            BindingContext = new RealtimeSchedulingViewModel();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Switch.IsToggled = false;
        }
    }
    
}
