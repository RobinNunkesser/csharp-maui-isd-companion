namespace ISDCompanion;

public partial class ExercisesPage : ContentPage
{
    public ExercisesPage()
    {
        InitializeComponent();
        BindingContext = new ExercisesViewModel(Navigation);
    }
}
