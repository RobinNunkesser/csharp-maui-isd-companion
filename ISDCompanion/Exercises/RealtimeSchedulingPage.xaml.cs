namespace ISDCompanion
{
    public partial class RealtimeSchedulingPage : ContentPage
    {
        private readonly RealtimeSchedulingViewModel viewModel = new();

        public RealtimeSchedulingPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

    }
}
