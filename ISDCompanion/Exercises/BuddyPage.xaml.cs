using ISDCompanion.Interfaces;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ISDCompanion
{
    public partial class BuddyPage : ContentPage, IAfterRender
    {
        protected IAfterRender ViewModelAfterRender = null;
        public void AfterRender()
        {
            ViewModelAfterRender.AfterRender();
        }
        public BuddyPage()
        {
            InitializeComponent();
            var vm = new BuddyViewModel();
            BindingContext = vm;
            ViewModelAfterRender = vm;
            vm.ScrollToPosition += (int x, int y, bool isAnimated) => { ScrollToPosition(x, y, isAnimated); };
        }

        private void ScrollToPosition(int x, int y, bool isAnimated)
        {
            var animation = new Animation(
                callback: y => scrollView.ScrollToAsync(x, y, animated: false),
                start: scrollView.ScrollY,
                end: y);

            animation.Commit(
                owner: this,
                name: "Scroll",
                length: 300,
                easing: Easing.SinInOut);
        }
    }
}
