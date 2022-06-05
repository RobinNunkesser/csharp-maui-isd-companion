using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ISDCompanion
{
    public partial class RealtimeSchedulingPage : ContentPage
    {
        public RealtimeSchedulingPage()
        {
            InitializeComponent();
            var vm = new RealtimeSchedulingViewModel();
            BindingContext = vm;

            vm.ScrollToPosition += (int x, int y) => { ScrollToPosition(x, y); };
        }

        private void ScrollToPosition(int x, int y)
        {
            scrollView.ScrollToAsync(x, y, true);
        }
    }
}
