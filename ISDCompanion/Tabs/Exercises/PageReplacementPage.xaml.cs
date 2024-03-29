﻿namespace StudyCompanion
{
    public partial class PageReplacementPage : ContentPage
    {
        private readonly PageReplacementViewModel viewModel = new();

        public PageReplacementPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.Initialize();
        }

    }
}