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

internal sealed record AINQueensSimulation(int BoardSize, IReadOnlyDictionary<int, int> InitialQueens, IReadOnlyList<AINQueensStep> Steps);

internal sealed class AINQueensSimulator
{
    public AINQueensSimulation Simulate(AINQueensAlgorithm algorithm, int boardSize, int scenarioIndex)
    {
        return algorithm == AINQueensAlgorithm.Backtracking
            ? SimulateBacktracking(boardSize)
            : SimulateMinConflicts(boardSize, scenarioIndex);
    }

    private AINQueensSimulation SimulateBacktracking(int boardSize)
    {
        var steps = new List<AINQueensStep>();
        var assignment = new Dictionary<int, int>();

        _ = Backtrack(0, boardSize, assignment, steps);

        return new AINQueensSimulation(boardSize, new Dictionary<int, int>(), steps);
    }

    private bool Backtrack(int column, int boardSize, Dictionary<int, int> assignment, List<AINQueensStep> steps)
    {
        if (column == boardSize)
        {
            steps.Add(CreateStep(assignment, null, null, AINQueensActionType.Solved, true));
            return true;
        }

        for (var row = 0; row < boardSize; row++)
        {
            assignment[column] = row;
            if (IsConsistent(assignment))
            {
                steps.Add(CreateStep(assignment, column, row, AINQueensActionType.Place, false));
                if (Backtrack(column + 1, boardSize, assignment, steps))
                {
                    return true;
                }
            }

            assignment.Remove(column);
        }

        steps.Add(CreateStep(assignment, column, null, AINQueensActionType.Backtrack, false));
        return false;
    }

    private AINQueensSimulation SimulateMinConflicts(int boardSize, int scenarioIndex)
    {
        var initial = CreateInitialBoard(boardSize, scenarioIndex);
        var current = new Dictionary<int, int>(initial);
        var steps = new List<AINQueensStep>();
        var maxSteps = Math.Max(boardSize * boardSize * 3, 40);

        for (var stepIndex = 0; stepIndex < maxSteps; stepIndex++)
        {
            var conflicted = GetConflictedColumns(current);
            if (conflicted.Count == 0)
            {
                steps.Add(CreateStep(current, null, null, AINQueensActionType.Solved, true));
                return new AINQueensSimulation(boardSize, initial, steps);
            }

            var column = conflicted.OrderBy(value => value).First();
            var row = GetBestRowForColumn(current, column);
            current[column] = row;
            steps.Add(CreateStep(current, column, row, AINQueensActionType.Move, GetConflictedColumns(current).Count == 0));

            if (GetConflictedColumns(current).Count == 0)
            {
                steps.Add(CreateStep(current, null, null, AINQueensActionType.Solved, true));
                return new AINQueensSimulation(boardSize, initial, steps);
            }
        }

        steps.Add(CreateStep(current, null, null, AINQueensActionType.Stopped, false));
        return new AINQueensSimulation(boardSize, initial, steps);
    }

    private static Dictionary<int, int> CreateInitialBoard(int boardSize, int scenarioIndex)
    {
        var shift = scenarioIndex % boardSize;
        var multiplier = boardSize % 2 == 0 ? 2 : 3;
        return Enumerable.Range(0, boardSize)
            .ToDictionary(index => index, index => ((index * multiplier) + shift + (index / 2)) % boardSize);
    }

    private int GetBestRowForColumn(Dictionary<int, int> assignment, int column)
    {
        var currentRow = assignment[column];
        var boardSize = assignment.Count;
        var rows = Enumerable.Range(0, boardSize)
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