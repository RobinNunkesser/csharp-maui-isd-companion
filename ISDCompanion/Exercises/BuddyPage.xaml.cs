using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ISDCompanion
{
    public partial class BuddyPage : ContentPage
    {
        public BuddyPage()
        {
            InitializeComponent();
            var vm = new BuddyViewModel();
            BindingContext = vm;

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
