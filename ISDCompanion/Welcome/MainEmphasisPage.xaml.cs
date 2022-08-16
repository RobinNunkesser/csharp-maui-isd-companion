using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ISDCompanion

{
    public partial class MainEmphasisPage : ContentPage
    {
        public MainEmphasisPage()
        {
            InitializeComponent();
            BindingContext = new MainEmphasisViewModel();
        }
    }
}
