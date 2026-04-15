using StudyCompanion.Resources.Strings;

namespace StudyCompanion;

public class AICrossValidationViewModel : StepwiseExerciseViewModel
{
    private readonly AICrossValidationSimulator _simulator = new();
    private AICrossValidationSimulation _simulation = new(string.Empty, string.Empty, 0, 0, 0, []);
    private string _stepText = string.Empty;
    private string _summaryText = string.Empty;
    private string _datasetText = string.Empty;
    private string _learnerText = string.Empty;
    private string _foldText = string.Empty;
    private string _thresholdText = string.Empty;
    private string _selectedParameterText = string.Empty;
    private string _currentParameterText = string.Empty;
    private string _trainingErrorText = string.Empty;
    private string _validationErrorText = string.Empty;

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

    public string LearnerText
    {
        get => _learnerText;
        set
        {
            _learnerText = value;
            OnPropertyChanged();
        }
    }

    public string FoldText
    {
        get => _foldText;
        set
        {
            _foldText = value;
            OnPropertyChanged();
        }
    }

    public string ThresholdText
    {
        get => _thresholdText;
        set
        {
            _thresholdText = value;
            OnPropertyChanged();
        }
    }

    public string SelectedParameterText
    {
        get => _selectedParameterText;
        set
        {
            _selectedParameterText = value;
            OnPropertyChanged();
        }
    }

    public string CurrentParameterText
    {
        get => _currentParameterText;
        set
        {
            _currentParameterText = value;
            OnPropertyChanged();
        }
    }

    public string TrainingErrorText
    {
        get => _trainingErrorText;
        set
        {
            _trainingErrorText = value;
            OnPropertyChanged();
        }
    }

    public string ValidationErrorText
    {
        get => _validationErrorText;
        set
        {
            _validationErrorText = value;
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

        DatasetText = $"{AppResources.LearningDataset}: {AppResources.LearningRestaurantDataset}";
        LearnerText = $"{AppResources.CrossValidationLearner}: {AppResources.CrossValidationDemoLearner}";
        FoldText = $"{AppResources.CrossValidationFolds}: {_simulation.FoldCount}";
        ThresholdText = $"{AppResources.CrossValidationThreshold}: {_simulation.Threshold:F2}";
        SelectedParameterText = $"{AppResources.CrossValidationSelectedParameter}: {_simulation.SelectedParameterSize}";

        if (_simulation.Steps.Count == 0 || CurrentSolutionStep == 0)
        {
            StepText = $"{AppResources.Step} 0/{_simulation.Steps.Count}";
            SummaryText = AppResources.PressNextToStart;
            CurrentParameterText = $"{AppResources.CrossValidationParameter}: -";
            TrainingErrorText = $"{AppResources.CrossValidationTrainingError}: -";
            ValidationErrorText = $"{AppResources.CrossValidationValidationError}: -";
            return;
        }

        var step = _simulation.Steps[CurrentSolutionStep - 1];
        StepText = $"{AppResources.Step} {CurrentSolutionStep}/{_simulation.Steps.Count}";
        SummaryText = step.Selected
            ? AppResources.CrossValidationBestChoice
            : AppResources.CrossValidationModelSelection;
        CurrentParameterText = $"{AppResources.CrossValidationParameter}: {step.ParameterSize}";
        TrainingErrorText = $"{AppResources.CrossValidationTrainingError}: {step.TrainingError:F2}";
        ValidationErrorText = $"{AppResources.CrossValidationValidationError}: {step.ValidationError:F2}";
    }
}