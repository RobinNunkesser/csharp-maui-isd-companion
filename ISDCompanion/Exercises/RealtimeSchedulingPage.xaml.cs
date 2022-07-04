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
            //object scrollView = FindByName("scrollView");
            //ScrollView scrollView2 = (ScrollView)scrollView;

            //var animation = new Animation(
            //    callback: x => scrollView2.ScrollToAsync(x, y, animated: false),
            //    start: scrollView2.ScrollX,
            //    end: x);

            //animation.Commit(
            //    owner: this,
            //    name: "Scroll",
            //    length: 300,
            //    easing: Easing.SinInOut);
        }
    }
}
