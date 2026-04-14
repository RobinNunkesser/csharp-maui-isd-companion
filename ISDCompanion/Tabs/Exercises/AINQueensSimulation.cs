using Italbytz.AI.CSP;

namespace StudyCompanion;

internal enum AINQueensAlgorithm
{
    Backtracking,
    MinConflicts
}

internal enum AINQueensActionType
{
    Initial,
    Place,
    Move,
    Backtrack,
    Solved,
    Stopped
}

internal sealed record AINQueensStep(
    IReadOnlyDictionary<int, int> Queens,
    int? CurrentColumn,
    int? CurrentRow,
    IReadOnlySet<int> ConflictedColumns,
    int ConflictCount,
    AINQueensActionType ActionType,
    bool Solved);

internal sealed record AINQueensSimulation(IReadOnlyDictionary<int, int> InitialQueens, IReadOnlyList<AINQueensStep> Steps);

internal sealed class AINQueensConstraint : IConstraint<Variable, int>
{
    private readonly int _leftColumn;
    private readonly int _rightColumn;

    public AINQueensConstraint(Variable left, int leftColumn, Variable right, int rightColumn)
    {
        Scope = [left, right];
        _leftColumn = leftColumn;
        _rightColumn = rightColumn;
    }

    public IList<Variable> Scope { get; }

    public bool IsSatisfiedWith(IAssignment<Variable, int> assignment)
    {
        if (!assignment.Contains(Scope[0]) || !assignment.Contains(Scope[1]))
        {
            return true;
        }

        var left = assignment.GetValue(Scope[0]);
        var right = assignment.GetValue(Scope[1]);

        return left != right && Math.Abs(left - right) != Math.Abs(_leftColumn - _rightColumn);
    }
}

internal sealed class AINQueensSimulator
{
    private const int BoardSize = 8;
    private static readonly int[][] MinConflictStartBoards =
    [
        [0, 0, 0, 0, 0, 0, 0, 0],
        [4, 5, 6, 3, 4, 5, 6, 5],
        [5, 7, 0, 1, 1, 7, 7, 2]
    ];

    private readonly Variable[] _variables = Enumerable.Range(0, BoardSize).Select(index => new Variable($"Q{index + 1}")).ToArray();
    private readonly Domain<int> _domain = new(Enumerable.Range(0, BoardSize).ToList());

    public AINQueensSimulation Simulate(AINQueensAlgorithm algorithm, int scenarioIndex)
    {
        return algorithm == AINQueensAlgorithm.Backtracking
            ? SimulateBacktracking()
            : SimulateMinConflicts(scenarioIndex);
    }

    private AINQueensSimulation SimulateBacktracking()
    {
        var steps = new List<AINQueensStep>();
        var assignment = new Dictionary<int, int>();

        _ = Backtrack(0, assignment, steps);

        return new AINQueensSimulation(new Dictionary<int, int>(), steps);
    }

    private bool Backtrack(int column, Dictionary<int, int> assignment, List<AINQueensStep> steps)
    {
        if (column == BoardSize)
        {
            steps.Add(CreateStep(assignment, null, null, AINQueensActionType.Solved, true));
            return true;
        }

        for (var row = 0; row < BoardSize; row++)
        {
            assignment[column] = row;
            if (IsConsistent(assignment))
            {
                steps.Add(CreateStep(assignment, column, row, AINQueensActionType.Place, false));
                if (Backtrack(column + 1, assignment, steps))
                {
                    return true;
                }
            }

            assignment.Remove(column);
        }

        steps.Add(CreateStep(assignment, column, null, AINQueensActionType.Backtrack, false));
        return false;
    }

    private AINQueensSimulation SimulateMinConflicts(int scenarioIndex)
    {
        var initial = CreateInitialBoard(scenarioIndex);
        var current = new Dictionary<int, int>(initial);
        var steps = new List<AINQueensStep>();

        for (var stepIndex = 0; stepIndex < 60; stepIndex++)
        {
            var conflicted = GetConflictedColumns(current);
            if (conflicted.Count == 0)
            {
                steps.Add(CreateStep(current, null, null, AINQueensActionType.Solved, true));
                return new AINQueensSimulation(initial, steps);
            }

            var column = conflicted.OrderBy(value => value).First();
            var row = GetBestRowForColumn(current, column);
            current[column] = row;
            steps.Add(CreateStep(current, column, row, AINQueensActionType.Move, GetConflictedColumns(current).Count == 0));

            if (GetConflictedColumns(current).Count == 0)
            {
                steps.Add(CreateStep(current, null, null, AINQueensActionType.Solved, true));
                return new AINQueensSimulation(initial, steps);
            }
        }

        steps.Add(CreateStep(current, null, null, AINQueensActionType.Stopped, false));
        return new AINQueensSimulation(initial, steps);
    }

    private Dictionary<int, int> CreateInitialBoard(int scenarioIndex)
    {
        var rows = MinConflictStartBoards[scenarioIndex % MinConflictStartBoards.Length];
        return Enumerable.Range(0, BoardSize).ToDictionary(index => index, index => rows[index]);
    }

    private int GetBestRowForColumn(Dictionary<int, int> assignment, int column)
    {
        var currentRow = assignment[column];
        var rows = Enumerable.Range(0, BoardSize)
            .Select(row => new { Row = row, Conflicts = CountConflictsForColumn(assignment, column, row) })
            .OrderBy(candidate => candidate.Conflicts)
            .ThenBy(candidate => candidate.Row == currentRow ? 1 : 0)
            .ThenBy(candidate => candidate.Row)
            .ToList();

        return rows[0].Row;
    }

    private int CountConflictsForColumn(Dictionary<int, int> assignment, int column, int row)
    {
        var conflicts = 0;
        foreach (var (otherColumn, otherRow) in assignment)
        {
            if (otherColumn == column)
            {
                continue;
            }

            if (Conflicts(column, row, otherColumn, otherRow))
            {
                conflicts++;
            }
        }

        return conflicts;
    }

    private static bool IsConsistent(Dictionary<int, int> assignment)
    {
        foreach (var (leftColumn, leftRow) in assignment)
        {
            foreach (var (rightColumn, rightRow) in assignment)
            {
                if (leftColumn >= rightColumn)
                {
                    continue;
                }

                if (Conflicts(leftColumn, leftRow, rightColumn, rightRow))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static HashSet<int> GetConflictedColumns(Dictionary<int, int> assignment)
    {
        var conflicted = new HashSet<int>();

        foreach (var (leftColumn, leftRow) in assignment)
        {
            foreach (var (rightColumn, rightRow) in assignment)
            {
                if (leftColumn >= rightColumn)
                {
                    continue;
                }

                if (Conflicts(leftColumn, leftRow, rightColumn, rightRow))
                {
                    conflicted.Add(leftColumn);
                    conflicted.Add(rightColumn);
                }
            }
        }

        return conflicted;
    }

    private static int CountConflictPairs(Dictionary<int, int> assignment)
    {
        var conflicts = 0;

        foreach (var (leftColumn, leftRow) in assignment)
        {
            foreach (var (rightColumn, rightRow) in assignment)
            {
                if (leftColumn >= rightColumn)
                {
                    continue;
                }

                if (Conflicts(leftColumn, leftRow, rightColumn, rightRow))
                {
                    conflicts++;
                }
            }
        }

        return conflicts;
    }

    private static bool Conflicts(int leftColumn, int leftRow, int rightColumn, int rightRow)
    {
        return leftRow == rightRow || Math.Abs(leftRow - rightRow) == Math.Abs(leftColumn - rightColumn);
    }

    private static AINQueensStep CreateStep(Dictionary<int, int> assignment, int? currentColumn, int? currentRow, AINQueensActionType actionType, bool solved)
    {
        var queens = new Dictionary<int, int>(assignment.OrderBy(entry => entry.Key).ToDictionary(entry => entry.Key, entry => entry.Value));
        var conflicted = GetConflictedColumns(queens);
        return new AINQueensStep(queens, currentColumn, currentRow, conflicted, CountConflictPairs(queens), actionType, solved);
    }
}