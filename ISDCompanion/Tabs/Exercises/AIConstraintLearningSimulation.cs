using Italbytz.AI.CSP.Examples;

namespace StudyCompanion;

internal sealed record AIConstraintLearningStep(
    int ExampleNumber,
    string ExampleText,
    bool IsPositive,
    string EliminatedHypotheses,
    string RemainingHypotheses,
    bool GoalReached);

internal sealed record AIConstraintLearningSimulation(
    string DatasetName,
    string TargetVariables,
    string HypothesisSpace,
    string LearnedConstraint,
    string CspTransfer,
    IReadOnlyList<AIConstraintLearningStep> Steps);

internal sealed class AIConstraintLearningSimulator
{
    private static readonly IReadOnlyList<ConstraintHypothesis> Hypotheses =
    [
        new("same color", (left, right) => left == right),
        new("different colors", (left, right) => left != right),
        new("left is red", (left, _) => left == MapCSP.RED),
        new("right is red", (_, right) => right == MapCSP.RED),
        new("at least one red", (left, right) => left == MapCSP.RED || right == MapCSP.RED),
        new("always valid", (_, _) => true)
    ];

    private static readonly IReadOnlyList<LabeledExample> Examples =
    [
        new(MapCSP.RED, MapCSP.GREEN, true),
        new(MapCSP.BLUE, MapCSP.BLUE, false),
        new(MapCSP.RED, MapCSP.RED, false)
    ];

    public AIConstraintLearningSimulation Simulate()
    {
        var remainingHypotheses = Hypotheses.ToList();
        var steps = new List<AIConstraintLearningStep>();

        for (var index = 0; index < Examples.Count; index++)
        {
            var example = Examples[index];
            var eliminated = remainingHypotheses
                .Where(hypothesis => hypothesis.Accepts(example.LeftColor, example.RightColor) != example.IsPositive)
                .ToList();

            remainingHypotheses = remainingHypotheses
                .Except(eliminated)
                .ToList();

            steps.Add(new AIConstraintLearningStep(
                index + 1,
                $"({example.LeftColor}, {example.RightColor})",
                example.IsPositive,
                FormatHypothesisList(eliminated.Select(hypothesis => hypothesis.Name)),
                FormatHypothesisList(remainingHypotheses.Select(hypothesis => hypothesis.Name)),
                remainingHypotheses.Count == 1));
        }

        var learnedConstraint = $"{MapCSP.WA.Name} != {MapCSP.NT.Name}";
        var cspTransfer = $"Use {learnedConstraint} as a NotEqualConstraint between neighboring regions.";

        return new AIConstraintLearningSimulation(
            "Binary color-pair examples",
            $"{MapCSP.WA.Name}, {MapCSP.NT.Name} with domain {{{MapCSP.RED}, {MapCSP.GREEN}, {MapCSP.BLUE}}}",
            FormatHypothesisList(Hypotheses.Select(hypothesis => hypothesis.Name)),
            learnedConstraint,
            cspTransfer,
            steps);
    }

    private static string FormatHypothesisList(IEnumerable<string> names)
    {
        var list = names.ToList();
        return list.Count == 0 ? "-" : string.Join(" | ", list);
    }

    private sealed record LabeledExample(string LeftColor, string RightColor, bool IsPositive);

    private sealed record ConstraintHypothesis(string Name, Func<string, string, bool> Accepts);
}