namespace StudyCompanion;

public partial class OperatingSystemsExercisesPage : ContentPage
{
    public OperatingSystemsExercisesPage()
    {
        InitializeComponent();
        BindingContext = new ExercisesViewModel(Navigation);
    }
}