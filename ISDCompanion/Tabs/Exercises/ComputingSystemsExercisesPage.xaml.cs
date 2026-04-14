namespace StudyCompanion;

public partial class ComputingSystemsExercisesPage : ContentPage
{
    public ComputingSystemsExercisesPage()
    {
        InitializeComponent();
        BindingContext = new ExercisesViewModel(Navigation);
    }
}