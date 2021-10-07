using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ISDCompanion
{
    public partial class SchedulingPage : ContentPage
    {
        public SchedulingPage()
        {
            InitializeComponent();
            BindingContext = new SchedulingViewModel();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            SwitchFCFS.IsToggled = false;
            SwitchPRIO.IsToggled = false;
            SwitchRR.IsToggled = false;
            SwitchSJF.IsToggled = false;
        }

    }
}
