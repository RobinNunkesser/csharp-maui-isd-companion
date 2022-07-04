using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ISDCompanion
{
    public partial class ExerciseBasicLayoutPage : ContentPage
    {
        public ExerciseBasicLayoutPage()
        {
            InitializeComponent();
            BindingContext = new ExerciseBasicLayoutViewModel();
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
    }
}
