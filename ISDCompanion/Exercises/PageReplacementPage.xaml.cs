namespace ISDCompanion
{
    public partial class PageReplacementPage : ContentPage
    {
        private readonly PageReplacementViewModel viewModel = new();

        public PageReplacementPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

    }
}