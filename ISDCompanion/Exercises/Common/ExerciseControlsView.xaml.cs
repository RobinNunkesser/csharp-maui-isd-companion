using System.Windows.Input;

namespace StudyCompanion;

public partial class ExerciseControlsView : ContentView
{
    public static readonly BindableProperty NewExerciseCommandProperty = BindableProperty.Create(nameof(NewExerciseCommand), typeof(ICommand), typeof(ExerciseControlsView), default(ICommand));
    public static readonly BindableProperty PreviousStepCommandProperty = BindableProperty.Create(nameof(PreviousStepCommand), typeof(ICommand), typeof(ExerciseControlsView), default(ICommand));
    public static readonly BindableProperty NextStepCommandProperty = BindableProperty.Create(nameof(NextStepCommand), typeof(ICommand), typeof(ExerciseControlsView), default(ICommand));
    public static readonly BindableProperty ShowSolutionCommandProperty = BindableProperty.Create(nameof(ShowSolutionCommand), typeof(ICommand), typeof(ExerciseControlsView), default(ICommand));

    public ICommand NewExerciseCommand
    {
        get => (ICommand)GetValue(NewExerciseCommandProperty);
        set => SetValue(NewExerciseCommandProperty, value);
    }

    public ICommand PreviousStepCommand
    {
        get => (ICommand)GetValue(PreviousStepCommandProperty);
        set => SetValue(PreviousStepCommandProperty, value);
    }

    public ICommand NextStepCommand
    {
        get => (ICommand)GetValue(NextStepCommandProperty);
        set => SetValue(NextStepCommandProperty, value);
    }

    public ICommand ShowSolutionCommand
    {
        get => (ICommand)GetValue(ShowSolutionCommandProperty);
        set => SetValue(ShowSolutionCommandProperty, value);
    }


    public ExerciseControlsView()
    {
        InitializeComponent();
        Content.BindingContext = this;
    }
}
