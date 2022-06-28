using ISDCompanion.Interfaces;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ISDCompanion
{
    public partial class RealtimeSchedulingPage : ContentPage, IAfterRender
    {
        protected IAfterRender ViewModelAfterRender = null;
        public void AfterRender()
        {
            ViewModelAfterRender.AfterRender();
        }
        public RealtimeSchedulingPage()
        {
            InitializeComponent();
            var vm = new RealtimeSchedulingViewModel();
            BindingContext = vm;
            ViewModelAfterRender = vm;
            vm.ScrollToPosition += (int x, int y, bool isAnimated) => { ScrollToPosition(x, y, isAnimated); };
        }

        private void ScrollToPosition(int x, int y, bool isAnimated)
        {
            var animation = new Animation(
                callback: x => scrollView.ScrollToAsync(x, y, animated: false),
                start: scrollView.ScrollX,
                end: x);

            animation.Commit(
                owner: this,
                name: "Scroll",
                length: 300,
                easing: Easing.SinInOut);
        }
    }
}
