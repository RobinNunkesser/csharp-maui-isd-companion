namespace ISDCompanion;

public partial class NetmaskPage : ContentPage
{
    private readonly NetmaskViewModel viewModel = new();

    public NetmaskPage()
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        SwitchNetwork.IsToggled = false;
        SwitchHost.IsToggled = false;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel.Initialize();
    }
}
