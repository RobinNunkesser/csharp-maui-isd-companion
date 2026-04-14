namespace StudyCompanion;

public partial class AIMapColoringPage : ContentPage
{
    private readonly AIMapColoringViewModel _viewModel = new();

    public AIMapColoringPage()
    {
        InitializeComponent();
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.Initialize();
    }
}