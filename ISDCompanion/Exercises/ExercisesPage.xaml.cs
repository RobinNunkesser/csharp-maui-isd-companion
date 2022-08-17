using ISDCompanion.Models;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ISDCompanion
{
    public partial class ExercisesPage : ContentPage
    {
        public ExercisesPage()
        {
            InitializeComponent();
            BindingContext = new ExercisesViewModel(Navigation);
        }
    }
}
