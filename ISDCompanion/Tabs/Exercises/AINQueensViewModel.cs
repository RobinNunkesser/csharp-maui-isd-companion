using StudyCompanion.Resources.Strings;

namespace StudyCompanion;

public class AINQueensViewModel : StepwiseExerciseViewModel
{
    private readonly AINQueensSimulator _simulator = new();
    private AINQueensSimulation _simulation = new(8, new Dictionary<int, int>(), new List<AINQueensStep>());
    private readonly int[] _boardSizes = [4, 8, 10];

    private int _selectedAlgorithmIndex;
    private int _selectedBoardSizeIndex = 1;
    private int _scenarioIndex;
    private View? _exerciseContent;
    private string _stepText = string.Empty;
    private string _summaryText = string.Empty;
    private string _currentAssignmentText = string.Empty;
    private string _conflictsText = string.Empty;

    public IReadOnlyList<string> Algorithms => [AppResources.NQueensBacktracking, AppResources.NQueensMinConflicts];

    public IReadOnlyList<string> BoardSizes => _boardSizes.Select(size => size.ToString()).ToArray();

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

    public int SelectedBoardSizeIndex
    {
        get => _selectedBoardSizeIndex;
        set
        {
            if (_selectedBoardSizeIndex == value)
            {
                return;
            }

            _selectedBoardSizeIndex = value;
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
        _simulation = _simulator.Simulate(CurrentAlgorithm, CurrentBoardSize, _scenarioIndex++);
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

    private AINQueensAlgorithm CurrentAlgorithm => SelectedAlgorithmIndex == 0
        ? AINQueensAlgorithm.Backtracking
        : AINQueensAlgorithm.MinConflicts;

    private int CurrentBoardSize => _boardSizes[_selectedBoardSizeIndex];

    private void RenderState()
    {
        RefreshCommandStates();

        if (_simulation.Steps.Count == 0 || CurrentSolutionStep == 0)
        {
            Exercise_Content = CreateBoardView(_simulation.BoardSize, _simulation.InitialQueens, GetConflictedColumns(_simulation.InitialQueens), null);
            StepText = $"{AppResources.Step} 0/{_simulation.Steps.Count}";
            SummaryText = AppResources.PressNextToStart;
            CurrentAssignmentText = $"{AppResources.CurrentAssignment}: {FormatAssignment(_simulation.InitialQueens)}";
            ConflictsText = $"{AppResources.Conflicts}: {CountConflicts(_simulation.InitialQueens)}";
            return;
        }

        var step = _simulation.Steps[CurrentSolutionStep - 1];
        Exercise_Content = CreateBoardView(_simulation.BoardSize, step.Queens, step.ConflictedColumns, step.CurrentColumn);
        StepText = step.Solved
            ? $"{AppResources.Step} {CurrentSolutionStep}/{_simulation.Steps.Count} - {AppResources.GoalReached}"
            : $"{AppResources.Step} {CurrentSolutionStep}/{_simulation.Steps.Count}";
        SummaryText = BuildSummary(step);
        CurrentAssignmentText = $"{AppResources.CurrentAssignment}: {FormatAssignment(step.Queens)}";
        ConflictsText = $"{AppResources.Conflicts}: {step.ConflictCount}";
    }

    private static View CreateBoardView(int boardSize, IReadOnlyDictionary<int, int> queens, IReadOnlySet<int> conflictedColumns, int? currentColumn)
    {
        return new GraphicsView
        {
            HeightRequest = 320,
            Drawable = new AINQueensBoardDrawable(boardSize, queens, conflictedColumns, currentColumn)
        };
    }

    private static string FormatAssignment(IReadOnlyDictionary<int, int> queens)
    {
        return queens.Count == 0
            ? "-"
            : string.Join(", ", queens.OrderBy(entry => entry.Key).Select(entry => $"Q{entry.Key + 1}->{entry.Value + 1}"));
    }

    private static string BuildSummary(AINQueensStep step)
    {
        return step.ActionType switch
        {
            AINQueensActionType.Place => $"{AppResources.PlaceQueen}: Q{step.CurrentColumn + 1} -> {step.CurrentRow + 1}",
            AINQueensActionType.Move => $"{AppResources.MoveQueen}: Q{step.CurrentColumn + 1} -> {step.CurrentRow + 1}",
            AINQueensActionType.Backtrack => $"{AppResources.Backtrack}: Q{step.CurrentColumn + 1}",
            AINQueensActionType.Solved => AppResources.SolutionFound,
            AINQueensActionType.Stopped => AppResources.MaxStepsReached,
            _ => AppResources.InitialBoard
        };
    }

    private static HashSet<int> GetConflictedColumns(IReadOnlyDictionary<int, int> queens)
    {
        var conflicted = new HashSet<int>();
        foreach (var (leftColumn, leftRow) in queens)
        {
            foreach (var (rightColumn, rightRow) in queens)
            {
                if (leftColumn >= rightColumn)
                {
                    continue;
                }

                if (leftRow == rightRow || Math.Abs(leftRow - rightRow) == Math.Abs(leftColumn - rightColumn))
                {
                    conflicted.Add(leftColumn);
                    conflicted.Add(rightColumn);
                }
            }
        }

        return conflicted;
    }

    private static int CountConflicts(IReadOnlyDictionary<int, int> queens)
    {
        var conflicts = 0;
        foreach (var (leftColumn, leftRow) in queens)
        {
            foreach (var (rightColumn, rightRow) in queens)
            {
                if (leftColumn >= rightColumn)
                {
                    continue;
                }

                if (leftRow == rightRow || Math.Abs(leftRow - rightRow) == Math.Abs(leftColumn - rightColumn))
                {
                    conflicts++;
                }
            }
        }

        return conflicts;
    }
}