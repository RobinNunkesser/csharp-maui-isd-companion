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
        }
    }
}
