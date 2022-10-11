namespace ISDCompanion
{
    public partial class BuddyPage : ContentPage
    {
        private readonly BuddyViewModel viewModel = new();

        public BuddyPage()
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
