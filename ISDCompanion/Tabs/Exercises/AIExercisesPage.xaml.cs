namespace StudyCompanion;

public partial class AIExercisesPage : ContentPage
{
    public AIExercisesPage()
    {
        InitializeComponent();
        BindingContext = new ExercisesViewModel(Navigation);
    }
}