﻿namespace StudyCompanion
{
    public partial class SchedulingPage : ContentPage
    {
        private readonly SchedulingViewModel viewModel = new();

        public SchedulingPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.ScrollToPosition += (int x, int y, bool isAnimated) => { Content.ScrollToPosition(x, y, isAnimated); };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.Initialize();
        }
    }
}

