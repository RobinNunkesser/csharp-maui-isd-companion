using ISDCompanion.Interfaces;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ISDCompanion
{
    public partial class BitencodingsPage : ContentPage, IAfterRender
    {
        protected IAfterRender ViewModelAfterRender = null;
        public void AfterRender()
        {
            ViewModelAfterRender.AfterRender();
        }

        public BitencodingsPage()
        {
            InitializeComponent();
            var vm = new BitencodingsViewModel();
            BindingContext = vm;
            ViewModelAfterRender = vm;
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
