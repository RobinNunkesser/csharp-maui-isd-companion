namespace ISDCompanion
{
    public partial class CRCPage : ContentPage
    {
        private readonly CRCViewModel viewModel = new();

        public CRCPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.ScrollToPosition += (int x, int y, bool isAnimated) => { Content.ScrollToPosition(x, y, isAnimated); };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.AfterRender();
        }

    }
}
