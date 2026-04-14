namespace StudyCompanion;

public partial class AINQueensPage : ContentPage
{
    private readonly AINQueensViewModel _viewModel = new();

    public AINQueensPage()
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