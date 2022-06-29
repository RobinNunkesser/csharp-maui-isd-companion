using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ISDCompanion
{
    public partial class ShortestPathsPage : ContentPage
    {
        public ShortestPathsPage()
        {
            InitializeComponent();
            BindingContext = new ShortestPathsViewModel();
        }
    }
}
