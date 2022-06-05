using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ISDCompanion
{
    public partial class PageReplacementPage : ContentPage
    {
        public PageReplacementPage()
        {
            InitializeComponent();
            var vm = new PageReplacementViewModel();
            BindingContext = vm;

            vm.ScrollToPosition += (int columnOfInterest) => { ScrollToPosition(columnOfInterest); };
        }

        private void ScrollToPosition(int columnOfInterest)
        {
            scrollView.ScrollToAsync(columnOfInterest, 0, false);
        }
    }
}
