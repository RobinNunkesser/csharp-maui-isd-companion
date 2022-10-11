namespace ISDCompanion
{
    public partial class AdditivesPage : ContentPage
    {
        public AdditivesPage()
        {
            InitializeComponent();
            BindingContext = new AdditivesViewModel();
        }
    }
}
