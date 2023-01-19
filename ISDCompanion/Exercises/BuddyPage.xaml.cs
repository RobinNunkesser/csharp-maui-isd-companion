namespace StudyCompanion
{
    public partial class BuddyPage : ContentPage
    {
        private readonly BuddyViewModel viewModel = new();

        public BuddyPage()
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
