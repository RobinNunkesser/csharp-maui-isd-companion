namespace StudyCompanion;

public partial class AIPlanningPage : ContentPage
{
    private readonly AIPlanningViewModel _viewModel = new();

    public AIPlanningPage()
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