namespace ISDCompanion;

public partial class MSTPage : ContentPage
{
    public MSTPage()
    {
        InitializeComponent();
        BindingContext = new MSTViewModel();
    }
}
