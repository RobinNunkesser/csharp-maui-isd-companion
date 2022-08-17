using ISDCompanion.Interfaces;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ISDCompanion
{
    public partial class CRCPage : ContentPage, IAfterRender
    {
        protected IAfterRender ViewModelAfterRender = null;

        public void AfterRender()
        {
            ViewModelAfterRender.AfterRender();

        }

        public CRCPage()
        {
            InitializeComponent(); 
            var vm = new CRCViewModel();
            BindingContext = vm;
            ViewModelAfterRender = vm;
            vm.ScrollToPosition += (int x, int y, bool isAnimated) => { Content.ScrollToPosition(x, y, isAnimated); };
        }
    }
}
