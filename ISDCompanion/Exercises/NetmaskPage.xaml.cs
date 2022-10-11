namespace ISDCompanion;

public partial class NetmaskPage : ContentPage
{
    public NetmaskPage()
    {
        InitializeComponent();
        BindingContext = new NetmaskViewModel();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        SwitchNetwork.IsToggled = false;
        SwitchHost.IsToggled = false;
    }
}
