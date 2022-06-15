using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ISDCompanion

{
    public partial class FirstOnInstallPage : ContentPage
    {
        public FirstOnInstallPage()
        {
            InitializeComponent();
            BindingContext = new FirstOnInstallViewModel();
        }
    }
}
