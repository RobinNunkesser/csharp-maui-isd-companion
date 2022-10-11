namespace ISDCompanion
{
    public partial class RealtimeSchedulingPage : ContentPage
    {
        private readonly RealtimeSchedulingViewModel viewModel = new();

        public RealtimeSchedulingPage()
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
