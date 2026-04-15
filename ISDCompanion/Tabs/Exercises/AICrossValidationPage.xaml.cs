namespace StudyCompanion;

public partial class AICrossValidationPage : ContentPage
{
    private readonly AICrossValidationViewModel _viewModel = new();

    public AICrossValidationPage()
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