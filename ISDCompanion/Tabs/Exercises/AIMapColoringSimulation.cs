using Italbytz.AI.CSP;
using Italbytz.AI.CSP.Examples;

namespace StudyCompanion;

internal enum AIMapColoringAlgorithm
{
    Backtracking,
    MinConflicts
}

internal enum AIMapColoringActionType
{
    Initial,
    Assign,
    Reassign,
    Backtrack,
    Solved,
    Stopped
}

internal sealed record AIMapColoringStep(
    IReadOnlyDictionary<string, string> Assignment,
    string? CurrentRegion,
    string? CurrentColor,
    IReadOnlySet<string> ConflictedRegions,
    int ConflictCount,
    AIMapColoringActionType ActionType,
    bool Solved);

internal sealed record AIMapColoringSimulation(IReadOnlyDictionary<string, string> InitialAssignment, IReadOnlyList<AIMapColoringStep> Steps);

internal sealed class AIMapColoringSimulator
{
    private static readonly string[][] MinConflictStartAssignments =
    [
        [MapCSP.RED, MapCSP.RED, MapCSP.RED, MapCSP.RED, MapCSP.RED, MapCSP.RED, MapCSP.BLUE],
        [MapCSP.GREEN, MapCSP.RED, MapCSP.GREEN, MapCSP.RED, MapCSP.GREEN, MapCSP.RED, MapCSP.BLUE],
        [MapCSP.BLUE, MapCSP.GREEN, MapCSP.BLUE, MapCSP.GREEN, MapCSP.RED, MapCSP.RED, MapCSP.RED]
    ];

    public AIMapColoringSimulation Simulate(AIMapColoringAlgorithm algorithm, int scenarioIndex)
    {
        return algorithm == AIMapColoringAlgorithm.Backtracking
            ? SimulateBacktracking()
            : SimulateMinConflicts(scenarioIndex);
    }

    private AIMapColoringSimulation SimulateBacktracking()
    {
        var csp = new MapCSP();
        var assignment = new Dictionary<Variable, string>();
        var steps = new List<AIMapColoringStep>();
        _ = Backtrack(csp, assignment, steps);
        return new AIMapColoringSimulation(new Dictionary<string, string>(), steps);
    }

    private bool Backtrack(MapCSP csp, Dictionary<Variable, string> assignment, List<AIMapColoringStep> steps)
    {
        if (assignment.Count == csp.Variables.Count)
        {
            steps.Add(CreateStep(csp, assignment, null, null, AIMapColoringActionType.Solved, true));
            return true;
        }

        var variable = csp.Variables.First(candidate => !assignment.ContainsKey(candidate));
        foreach (var color in csp.GetDomain(variable))
        {
            assignment[variable] = color;
            if (IsConsistent(csp, assignment, variable))
            {
                steps.Add(CreateStep(csp, assignment, variable.Name, color, AIMapColoringActionType.Assign, false));
                if (Backtrack(csp, assignment, steps))
                {
                    return true;
                }
            }

            assignment.Remove(variable);
        }

        steps.Add(CreateStep(csp, assignment, variable.Name, null, AIMapColoringActionType.Backtrack, false));
        return false;
    }

    private AIMapColoringSimulation SimulateMinConflicts(int scenarioIndex)
    {
        var csp = new MapCSP();
        var initialAssignment = CreateInitialAssignment(csp, scenarioIndex);
        var current = new Dictionary<Variable, string>(initialAssignment);
        var steps = new List<AIMapColoringStep>();

        for (var stepIndex = 0; stepIndex < 40; stepIndex++)
        {
            var conflicted = GetConflictedVariables(csp, current);
            if (conflicted.Count == 0)
            {
                steps.Add(CreateStep(csp, current, null, null, AIMapColoringActionType.Solved, true));
                return new AIMapColoringSimulation(ToRegionAssignment(initialAssignment), steps);
            }

            var variable = conflicted.OrderBy(candidate => csp.Variables.IndexOf(candidate)).First();
            var color = GetBestColor(csp, current, variable);
            current[variable] = color;
            steps.Add(CreateStep(csp, current, variable.Name, color, AIMapColoringActionType.Reassign, GetConflictedVariables(csp, current).Count == 0));
        }

        steps.Add(CreateStep(csp, current, null, null, AIMapColoringActionType.Stopped, false));
        return new AIMapColoringSimulation(ToRegionAssignment(initialAssignment), steps);
    }

    private static Dictionary<Variable, string> CreateInitialAssignment(MapCSP csp, int scenarioIndex)
    {
        var values = MinConflictStartAssignments[scenarioIndex % MinConflictStartAssignments.Length];
        return csp.Variables.Select((variable, index) => new { variable, value = values[index] }).ToDictionary(entry => entry.variable, entry => entry.value);
    }

    private static bool IsConsistent(MapCSP csp, Dictionary<Variable, string> assignment, Variable variable)
    {
        foreach (var constraint in csp.GetConstraints(variable))
        {
            var left = constraint.Scope[0];
            var right = constraint.Scope[1];
            if (!assignment.ContainsKey(left) || !assignment.ContainsKey(right))
            {
                continue;
            }

            if (assignment[left] == assignment[right])
            {
                return false;
            }
        }

        return true;
    }

    private static HashSet<Variable> GetConflictedVariables(MapCSP csp, Dictionary<Variable, string> assignment)
    {
        var conflicted = new HashSet<Variable>();
        foreach (var constraint in csp.Constraints)
        {
            var left = constraint.Scope[0];
            var right = constraint.Scope[1];
            if (!assignment.ContainsKey(left) || !assignment.ContainsKey(right))
            {
                continue;
            }

            if (assignment[left] == assignment[right])
            {
                conflicted.Add(left);
                conflicted.Add(right);
            }
        }

        return conflicted;
    }

    private static string GetBestColor(MapCSP csp, Dictionary<Variable, string> assignment, Variable variable)
    {
        var currentColor = assignment[variable];
        var bestConflictCount = int.MaxValue;
        var bestColor = currentColor;

        foreach (var color in csp.GetDomain(variable))
        {
            assignment[variable] = color;
            var conflictCount = CountConflicts(csp, assignment, variable);
            if (conflictCount < bestConflictCount || (conflictCount == bestConflictCount && color != currentColor && string.CompareOrdinal(color, bestColor) < 0))
            {
                bestConflictCount = conflictCount;
                bestColor = color;
            }
        }

        assignment[variable] = bestColor;
        return bestColor;
    }

    private static int CountConflicts(MapCSP csp, Dictionary<Variable, string> assignment, Variable variable)
    {
        return csp.GetConstraints(variable)
            .Count(constraint => assignment.ContainsKey(constraint.Scope[0]) && assignment.ContainsKey(constraint.Scope[1]) && assignment[constraint.Scope[0]] == assignment[constraint.Scope[1]]);
    }

    private static AIMapColoringStep CreateStep(MapCSP csp, Dictionary<Variable, string> assignment, string? currentRegion, string? currentColor, AIMapColoringActionType actionType, bool solved)
    {
        var regionAssignment = ToRegionAssignment(assignment);
        var conflicted = GetConflictedVariables(csp, assignment).Select(variable => variable.Name).ToHashSet();
        var conflictCount = csp.Constraints.Count(constraint => assignment.ContainsKey(constraint.Scope[0]) && assignment.ContainsKey(constraint.Scope[1]) && assignment[constraint.Scope[0]] == assignment[constraint.Scope[1]]);
        return new AIMapColoringStep(regionAssignment, currentRegion, currentColor, conflicted, conflictCount, actionType, solved);
    }

    private static Dictionary<string, string> ToRegionAssignment(IEnumerable<KeyValuePair<Variable, string>> assignment)
    {
        return assignment.ToDictionary(entry => entry.Key.Name, entry => entry.Value);
    }
}