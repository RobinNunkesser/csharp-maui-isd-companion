using StudyCompanion.Resources.Strings;

namespace StudyCompanion;

public class AIPlanningViewModel : StepwiseExerciseViewModel
{
    private readonly AIPlanningSimulator _simulator = new();
    private AIPlanningSimulation _simulation = new([], [], [], [], []);
    private int _selectedScenarioIndex;

    private string _stepText = string.Empty;
    private string _summaryText = string.Empty;
    private string _scenarioText = string.Empty;
    private string _algorithmText = string.Empty;
    private string _initialStateText = string.Empty;
    private string _goalText = string.Empty;
    private string _currentStateText = string.Empty;
    private string _availableActionsText = string.Empty;
    private string _planText = string.Empty;
    private string _appliedActionText = string.Empty;
    private string _goalStatusText = string.Empty;

    public IReadOnlyList<string> Scenarios =>
    [
        AppResources.PlanningScenarioGoHomeToSfo,
        AppResources.PlanningScenarioParcelToLab
    ];

    public int SelectedScenarioIndex
    {
        get => _selectedScenarioIndex;
        set
        {
            if (_selectedScenarioIndex == value)
            {
                return;
            }

            _selectedScenarioIndex = value;
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

    public string ScenarioText
    {
        get => _scenarioText;
        set
        {
            _scenarioText = value;
            OnPropertyChanged();
        }
    }

    public string AlgorithmText
    {
        get => _algorithmText;
        set
        {
            _algorithmText = value;
            OnPropertyChanged();
        }
    }

    public string InitialStateText
    {
        get => _initialStateText;
        set
        {
            _initialStateText = value;
            OnPropertyChanged();
        }
    }

    public string GoalText
    {
        get => _goalText;
        set
        {
            _goalText = value;
            OnPropertyChanged();
        }
    }

    public string CurrentStateText
    {
        get => _currentStateText;
        set
        {
            _currentStateText = value;
            OnPropertyChanged();
        }
    }

    public string AvailableActionsText
    {
        get => _availableActionsText;
        set
        {
            _availableActionsText = value;
            OnPropertyChanged();
        }
    }

    public string PlanText
    {
        get => _planText;
        set
        {
            _planText = value;
            OnPropertyChanged();
        }
    }

    public string AppliedActionText
    {
        get => _appliedActionText;
        set
        {
            _appliedActionText = value;
            OnPropertyChanged();
        }
    }

    public string GoalStatusText
    {
        get => _goalStatusText;
        set
        {
            _goalStatusText = value;
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
        _simulation = _simulator.Simulate(CurrentScenario);
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

        ScenarioText = $"{AppResources.PlanningScenario}: {CurrentScenarioName}";
        AlgorithmText = $"{AppResources.PlanningAlgorithm}: {AppResources.PlanningAlgorithmHierarchical}";
        InitialStateText = $"{AppResources.PlanningInitialState}: {FormatValues(_simulation.InitialState)}";
        GoalText = $"{AppResources.PlanningGoal}: {FormatValues(_simulation.Goal)}";

        if (_simulation.Steps.Count == 0 || CurrentSolutionStep == 0)
        {
            StepText = $"{AppResources.Step} 0/{_simulation.Steps.Count}";
            SummaryText = AppResources.PressNextToStart;
            CurrentStateText = $"{AppResources.PlanningCurrentState}: {FormatValues(_simulation.InitialState)}";
            AvailableActionsText = $"{AppResources.PlanningAvailableActions}: {FormatValues(_simulation.InitialAvailableActions)}";
            PlanText = $"{AppResources.PlanningPlan}: {FormatValues(_simulation.PlannedActions)}";
            AppliedActionText = $"{AppResources.PlanningAppliedAction}: {AppResources.PlanningNoActionYet}";
            GoalStatusText = $"{AppResources.PlanningGoalStatus}: {AppResources.PlanningGoalPending}";
            return;
        }

        var step = _simulation.Steps[CurrentSolutionStep - 1];
        StepText = step.GoalReached
            ? $"{AppResources.Step} {CurrentSolutionStep}/{_simulation.Steps.Count} - {AppResources.GoalReached}"
            : $"{AppResources.Step} {CurrentSolutionStep}/{_simulation.Steps.Count}";
        SummaryText = step.GoalReached
            ? AppResources.PlanningGoalSatisfied
            : step.AppliedAction;
        CurrentStateText = $"{AppResources.PlanningCurrentState}: {FormatValues(step.CurrentState)}";
        AvailableActionsText = $"{AppResources.PlanningAvailableActions}: {FormatValues(step.AvailableActions)}";
        PlanText = $"{AppResources.PlanningPlan}: {FormatPlan(step.ExecutedPlan, step.RemainingPlan)}";
        AppliedActionText = $"{AppResources.PlanningAppliedAction}: {step.AppliedAction}";
        GoalStatusText = $"{AppResources.PlanningGoalStatus}: {(step.GoalReached ? AppResources.PlanningGoalSatisfied : AppResources.PlanningGoalPending)}";
    }

    private static string FormatValues(IReadOnlyList<string> values)
    {
        return values.Count == 0
            ? "-"
            : string.Join(" | ", values);
    }

    private static string FormatPlan(IReadOnlyList<string> executedPlan, IReadOnlyList<string> remainingPlan)
    {
        if (executedPlan.Count == 0 && remainingPlan.Count == 0)
        {
            return "-";
        }

        if (remainingPlan.Count == 0)
        {
            return string.Join(" -> ", executedPlan);
        }

        if (executedPlan.Count == 0)
        {
            return string.Join(" -> ", remainingPlan);
        }

        return $"{string.Join(" -> ", executedPlan)} | {string.Join(" -> ", remainingPlan)}";
    }

    private AIPlanningScenario CurrentScenario => SelectedScenarioIndex == 1
        ? AIPlanningScenario.ParcelToLab
        : AIPlanningScenario.GoHomeToSfo;

    private string CurrentScenarioName => CurrentScenario == AIPlanningScenario.ParcelToLab
        ? AppResources.PlanningScenarioParcelToLab
        : AppResources.PlanningScenarioGoHomeToSfo;
}