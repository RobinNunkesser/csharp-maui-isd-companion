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

            vm.ScrollToPosition += (int x, int y, bool isAnimated) => { ScrollToPosition(x, y, isAnimated); };
        }

        private void ScrollToPosition(int x, int y, bool isAnimated)
        {
            scrollView.ScrollToAsync(x, y, isAnimated);
        }
    }
}