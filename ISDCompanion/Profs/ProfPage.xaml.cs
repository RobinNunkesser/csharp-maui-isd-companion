﻿namespace ISDCompanion
{
    public partial class ProfPage : ContentPage
    {
        public ProfPage(string source)
        {
            InitializeComponent();
            Browser.Source = source;
        }
    }
}
