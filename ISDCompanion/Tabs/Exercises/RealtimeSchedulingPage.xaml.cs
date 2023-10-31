namespace StudyCompanion
{
    public partial class RealtimeSchedulingPage : ContentPage
    {
        private readonly RealtimeSchedulingViewModel viewModel = new();

        public RealtimeSchedulingPage()
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
