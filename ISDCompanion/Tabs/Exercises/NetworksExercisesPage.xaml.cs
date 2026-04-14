namespace StudyCompanion;

public partial class NetworksExercisesPage : ContentPage
{
    public NetworksExercisesPage()
    {
        InitializeComponent();
        BindingContext = new ExercisesViewModel(Navigation);
    }
}