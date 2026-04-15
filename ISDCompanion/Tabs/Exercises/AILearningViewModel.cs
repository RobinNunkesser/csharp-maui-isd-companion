using StudyCompanion.Resources.Strings;

namespace StudyCompanion;

public class AILearningViewModel : StepwiseExerciseViewModel
{
    private readonly AILearningSimulator _simulator = new();
    private AILearningSimulation _simulation = new(string.Empty, string.Empty, 0, 0, 0, 0, string.Empty, [], []);
    private int _selectedLearnerIndex;
    private string _stepText = string.Empty;
    private string _summaryText = string.Empty;
    private string _learnerText = string.Empty;
    private string _datasetText = string.Empty;
    private string _accuracyText = string.Empty;
    private string _bestAttributeText = string.Empty;
    private string _stumpCountText = string.Empty;
    private string _gainsText = string.Empty;
    private string _exampleText = string.Empty;
    private string _predictionText = string.Empty;

    public IReadOnlyList<string> Learners =>
    [
        AppResources.MajorityLearnerDemo,
        AppResources.DecisionTreeLearnerDemo
    ];

    public int SelectedLearnerIndex
    {
        get => _selectedLearnerIndex;
        set
        {
            if (_selectedLearnerIndex == value)
            {
                return;
            }

            _selectedLearnerIndex = value;
            OnPropertyChanged();
            newExercise();
        }
    }

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

    public string LearnerText
    {
        get => _learnerText;
        set
        {
            _learnerText = value;
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

    public string AccuracyText
    {
        get => _accuracyText;
        set
        {
            _accuracyText = value;
            OnPropertyChanged();
        }
    }

    public string BestAttributeText
    {
        get => _bestAttributeText;
        set
        {
            _bestAttributeText = value;
            OnPropertyChanged();
        }
    }

    public string StumpCountText
    {
        get => _stumpCountText;
        set
        {
            _stumpCountText = value;
            OnPropertyChanged();
        }
    }

    public string GainsText
    {
        get => _gainsText;
        set
        {
            _gainsText = value;
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

    public string PredictionText
    {
        get => _predictionText;
        set
        {
            _predictionText = value;
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
        _simulation = _simulator.Simulate(CurrentLearner);
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

        LearnerText = $"{AppResources.Learner}: {CurrentLearnerName}";
        DatasetText = $"{AppResources.LearningDataset}: {AppResources.LearningRestaurantDataset}";
        AccuracyText = $"{AppResources.LearningAccuracy}: {_simulation.CorrectCount}/{_simulation.ExampleCount}";
        BestAttributeText = $"{AppResources.LearningBestAttribute}: {_simulation.BestAttribute}";
        StumpCountText = $"{AppResources.LearningStumpCount}: {_simulation.StumpCount}";
        GainsText = $"{AppResources.LearningAttributeGains}: {string.Join(" | ", _simulation.Gains)}";

        if (_simulation.Steps.Count == 0 || CurrentSolutionStep == 0)
        {
            StepText = $"{AppResources.Step} 0/{_simulation.Steps.Count}";
            SummaryText = AppResources.PressNextToStart;
            ExampleText = $"{AppResources.LearningExample}: -";
            PredictionText = $"{AppResources.LearningPrediction}: -";
            return;
        }

        var step = _simulation.Steps[CurrentSolutionStep - 1];
        StepText = $"{AppResources.Step} {CurrentSolutionStep}/{_simulation.Steps.Count}";
        SummaryText = step.IsCorrect
            ? AppResources.LearningCorrectPrediction
            : AppResources.LearningWrongPrediction;
        ExampleText = $"{AppResources.LearningExample} {step.ExampleNumber}: {step.Attributes}";
        PredictionText = $"{AppResources.LearningPrediction}: {step.PredictedValue} | {AppResources.LearningActual}: {step.ActualValue}";
    }

    private AILearningLearner CurrentLearner => SelectedLearnerIndex == 1
        ? AILearningLearner.DecisionTree
        : AILearningLearner.Majority;

    private string CurrentLearnerName => CurrentLearner == AILearningLearner.DecisionTree
        ? AppResources.DecisionTreeLearnerDemo
        : AppResources.MajorityLearnerDemo;
}