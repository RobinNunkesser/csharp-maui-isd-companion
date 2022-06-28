using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ISDCompanion

{
    public partial class SemesterPage : ContentPage
    {
        public SemesterPage()
        {
            InitializeComponent();
            BindingContext = new SemesterViewModel();
        }
    }
}
