namespace ISDCompanion
{
    public partial class ShortestPathsPage : ContentPage
    {
        private readonly ShortestPathsViewModel viewModel = new();

        public ShortestPathsPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Switch.IsToggled = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.Initialize();
        }
    }
}
