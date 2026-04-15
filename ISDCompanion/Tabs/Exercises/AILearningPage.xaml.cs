namespace StudyCompanion;

public partial class AILearningPage : ContentPage
{
    private readonly AILearningViewModel _viewModel = new();

    public AILearningPage()
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