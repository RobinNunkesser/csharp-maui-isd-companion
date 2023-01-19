namespace StudyCompanion;

public partial class MSTPage : ContentPage
{
    private readonly MSTViewModel viewModel = new();

    public MSTPage()
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
