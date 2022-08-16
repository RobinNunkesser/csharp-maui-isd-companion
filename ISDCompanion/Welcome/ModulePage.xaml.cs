using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ISDCompanion

{
    public partial class ModulePage : ContentPage
    {
        public ModulePage()
        {
            InitializeComponent();
            BindingContext = new ModuleViewModel(Navigation);
        }
    }
}
