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
            SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            if (Width > Height)
            {
                Shell.SetTabBarIsVisible(this, false);
            }
            else
            {
                Shell.SetTabBarIsVisible(this, true);
            }
        }

        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    Switch.IsToggled = false;
        //}
    }
}
