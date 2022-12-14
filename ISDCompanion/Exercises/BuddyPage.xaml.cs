namespace ISDCompanion
{
    public partial class BuddyPage : ContentPage
    {
        private readonly BuddyViewModel viewModel = new();

        public BuddyPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

    }
}
