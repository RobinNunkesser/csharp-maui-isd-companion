using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ISDCompanion

{
    public partial class StatusPage : ContentPage
    {
        public StatusPage()
        {
            InitializeComponent();
            BindingContext = new StatusPage();
        }
    }
}
