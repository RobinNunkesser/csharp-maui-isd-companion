namespace StudyCompanion;

public partial class AIConstraintLearningPage : ContentPage
{
    private readonly AIConstraintLearningViewModel _viewModel = new();

    public AIConstraintLearningPage()
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