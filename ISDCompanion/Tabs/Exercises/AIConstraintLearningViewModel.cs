using StudyCompanion.Resources.Strings;

namespace StudyCompanion;

public class AIConstraintLearningViewModel : StepwiseExerciseViewModel
{
    private readonly AIConstraintLearningSimulator _simulator = new();
    private AIConstraintLearningSimulation _simulation = new(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, []);
    private string _stepText = string.Empty;
    private string _summaryText = string.Empty;
    private string _datasetText = string.Empty;
    private string _variablesText = string.Empty;
    private string _hypothesisSpaceText = string.Empty;
    private string _learnedConstraintText = string.Empty;
    private string _transferText = string.Empty;
    private string _exampleText = string.Empty;
    private string _eliminatedText = string.Empty;
    private string _remainingText = string.Empty;

    public string StepText
    {
        get => _stepText;
        set
        {
            _stepText = value;
            OnPropertyChanged();
        }
    }

    public string SummaryText
    {
        get => _summaryText;
        set
        {
            _summaryText = value;
            OnPropertyChanged();
        }
    }

    public string DatasetText
    {
        get => _datasetText;
        set
        {
            _datasetText = value;
            OnPropertyChanged();
        }
    }

    public string VariablesText
    {
        get => _variablesText;
        set
        {
            _variablesText = value;
            OnPropertyChanged();
        }
    }

    public string HypothesisSpaceText
    {
        get => _hypothesisSpaceText;
        set
        {
            _hypothesisSpaceText = value;
            OnPropertyChanged();
        }
    }

    public string LearnedConstraintText
    {
        get => _learnedConstraintText;
        set
        {
            _learnedConstraintText = value;
            OnPropertyChanged();
        }
    }

    public string TransferText
    {
        get => _transferText;
        set
        {
            _transferText = value;
            OnPropertyChanged();
        }
    }

    public string ExampleText
    {
        get => _exampleText;
        set
        {
            _exampleText = value;
            OnPropertyChanged();
        }
    }

    public string EliminatedText
    {
        get => _eliminatedText;
        set
        {
            _eliminatedText = value;
            OnPropertyChanged();
        }
    }

    public string RemainingText
    {
        get => _remainingText;
        set
        {
            _remainingText = value;
            OnPropertyChanged();
        }
    }

    public override void Initialize()
    {
        newExercise();
    }

    protected override void newExercise()
    {
        CurrentSolutionStep = 0;
        _simulation = _simulator.Simulate();
        RenderState();
    }

    protected override void previousStep()
    {
        if (CurrentSolutionStep == 0)
        {
            return;
        }

        CurrentSolutionStep--;
        RenderState();
    }

    protected override void nextStep()
    {
        if (CurrentSolutionStep >= _simulation.Steps.Count)
        {
            return;
        }

        CurrentSolutionStep++;
        RenderState();
    }

    protected override void showCompleteSolution()
    {
        CurrentSolutionStep = _simulation.Steps.Count;
        RenderState();
    }

    protected override void showInfo()
    {
    }

    protected override bool CanMoveToNextStep() => _simulation.Steps.Count > 0 && CurrentSolutionStep < _simulation.Steps.Count;

    protected override bool CanMoveToPreviousStep() => CurrentSolutionStep > 0;

    protected override bool CanShowCompleteSolution() => _simulation.Steps.Count > 0 && CurrentSolutionStep < _simulation.Steps.Count;

    protected override bool CanShowInfo() => false;

    private void RenderState()
    {
        RefreshCommandStates();

        DatasetText = $"{AppResources.ConstraintLearningDataset}: {AppResources.ConstraintLearningBinaryColorDataset}";
        VariablesText = $"{AppResources.ConstraintLearningVariables}: {_simulation.TargetVariables}";
        HypothesisSpaceText = $"{AppResources.ConstraintLearningHypothesisSpace}: {_simulation.HypothesisSpace}";
        LearnedConstraintText = $"{AppResources.ConstraintLearningLearnedConstraint}: {_simulation.LearnedConstraint}";
        TransferText = $"{AppResources.ConstraintLearningCspTransfer}: {_simulation.CspTransfer}";

        if (_simulation.Steps.Count == 0 || CurrentSolutionStep == 0)
        {
            StepText = $"{AppResources.Step} 0/{_simulation.Steps.Count}";
            SummaryText = AppResources.PressNextToStart;
            ExampleText = $"{AppResources.ConstraintLearningExample}: -";
            EliminatedText = $"{AppResources.ConstraintLearningEliminated}: -";
            RemainingText = $"{AppResources.ConstraintLearningRemaining}: -";
            return;
        }

        var step = _simulation.Steps[CurrentSolutionStep - 1];
        var label = step.IsPositive
            ? AppResources.ConstraintLearningPositiveExample
            : AppResources.ConstraintLearningNegativeExample;

        StepText = $"{AppResources.Step} {CurrentSolutionStep}/{_simulation.Steps.Count}";
        SummaryText = step.GoalReached
            ? AppResources.ConstraintLearningConstraintFound
            : AppResources.ConstraintLearningFilterHypotheses;
        ExampleText = $"{AppResources.ConstraintLearningExample} {step.ExampleNumber}: {step.ExampleText} -> {label}";
        EliminatedText = $"{AppResources.ConstraintLearningEliminated}: {step.EliminatedHypotheses}";
        RemainingText = $"{AppResources.ConstraintLearningRemaining}: {step.RemainingHypotheses}";
    }
}