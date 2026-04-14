namespace StudyCompanion;

public partial class AIRomaniaSearchPage : ContentPage
{
    private readonly AIRomaniaSearchViewModel _viewModel = new();

    public AIRomaniaSearchPage()
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