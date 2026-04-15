using StudyCompanion.Resources.Strings;

namespace StudyCompanion;

public class AIMapColoringViewModel : StepwiseExerciseViewModel
{
    private readonly AIMapColoringSimulator _simulator = new();
    private AIMapColoringSimulation _simulation = new(new Dictionary<string, string>(), new List<AIMapColoringStep>());

    private int _selectedAlgorithmIndex;
    private int _scenarioIndex;
    private View? _exerciseContent;
    private string _stepText = string.Empty;
    private string _summaryText = string.Empty;
    private string _currentAssignmentText = string.Empty;
    private string _conflictsText = string.Empty;

    public IReadOnlyList<string> Algorithms => [AppResources.NQueensBacktracking, AppResources.NQueensMinConflicts];

    public int SelectedAlgorithmIndex
    {
        get => _selectedAlgorithmIndex;
        set
        {
            if (_selectedAlgorithmIndex == value)
            {
                return;
            }

            _selectedAlgorithmIndex = value;
            OnPropertyChanged();
            newExercise();
        }
    }

    public View? Exercise_Content
    {
        get => _exerciseContent;
        set
        {
            _exerciseContent = value;
            OnPropertyChanged();
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

    public string CurrentAssignmentText
    {
        get => _currentAssignmentText;
        set
        {
            _currentAssignmentText = value;
            OnPropertyChanged();
        }
    }

    public string ConflictsText
    {
        get => _conflictsText;
        set
        {
            _conflictsText = value;
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
        _simulation = _simulator.Simulate(CurrentAlgorithm, _scenarioIndex++);
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

    private AIMapColoringAlgorithm CurrentAlgorithm => SelectedAlgorithmIndex == 0
        ? AIMapColoringAlgorithm.Backtracking
        : AIMapColoringAlgorithm.MinConflicts;

    private void RenderState()
    {
        RefreshCommandStates();

        if (_simulation.Steps.Count == 0 || CurrentSolutionStep == 0)
        {
            Exercise_Content = CreateMapView(_simulation.InitialAssignment, new HashSet<string>(), null);
            StepText = $"{AppResources.Step} 0/{_simulation.Steps.Count}";
            SummaryText = AppResources.PressNextToStart;
            CurrentAssignmentText = $"{AppResources.CurrentAssignment}: {FormatAssignment(_simulation.InitialAssignment)}";
            ConflictsText = $"{AppResources.Conflicts}: 0";
            return;
        }

        var step = _simulation.Steps[CurrentSolutionStep - 1];
        Exercise_Content = CreateMapView(step.Assignment, step.ConflictedRegions, step.CurrentRegion);
        StepText = step.Solved
            ? $"{AppResources.Step} {CurrentSolutionStep}/{_simulation.Steps.Count} - {AppResources.GoalReached}"
            : $"{AppResources.Step} {CurrentSolutionStep}/{_simulation.Steps.Count}";
        SummaryText = BuildSummary(step);
        CurrentAssignmentText = $"{AppResources.CurrentAssignment}: {FormatAssignment(step.Assignment)}";
        ConflictsText = $"{AppResources.Conflicts}: {step.ConflictCount}";
    }

    private static View CreateMapView(IReadOnlyDictionary<string, string> assignment, IReadOnlySet<string> conflictedRegions, string? currentRegion)
    {
        return new GraphicsView
        {
            HeightRequest = 340,
            Drawable = new AIMapColoringDrawable(assignment, conflictedRegions, currentRegion)
        };
    }

    private static string FormatAssignment(IReadOnlyDictionary<string, string> assignment)
    {
        return assignment.Count == 0
            ? "-"
            : string.Join(", ", assignment.OrderBy(entry => entry.Key).Select(entry => $"{entry.Key}={entry.Value}"));
    }

    private static string BuildSummary(AIMapColoringStep step)
    {
        return step.ActionType switch
        {
            AIMapColoringActionType.Assign => $"{AppResources.ColorRegion}: {step.CurrentRegion} -> {step.CurrentColor}",
            AIMapColoringActionType.Reassign => $"{AppResources.RecolorRegion}: {step.CurrentRegion} -> {step.CurrentColor}",
            AIMapColoringActionType.Backtrack => $"{AppResources.Backtrack}: {step.CurrentRegion}",
            AIMapColoringActionType.Solved => AppResources.SolutionFound,
            AIMapColoringActionType.Stopped => AppResources.MaxStepsReached,
            _ => AppResources.InitialBoard
        };
    }
}