using Italbytz.AI.Learning;
using Italbytz.AI.Learning.Inductive;
using Italbytz.AI.Learning.Learners;

namespace StudyCompanion;

internal enum AILearningLearner
{
    Majority,
    DecisionTree
}

internal sealed record AILearningStep(
    int ExampleNumber,
    string Attributes,
    string ActualValue,
    string PredictedValue,
    bool IsCorrect);

internal sealed record AILearningSimulation(
    string LearnerName,
    string DatasetName,
    int ExampleCount,
    int CorrectCount,
    int WrongCount,
    int StumpCount,
    string BestAttribute,
    IReadOnlyList<string> Gains,
    IReadOnlyList<AILearningStep> Steps);

internal sealed class AILearningSimulator
{
    public AILearningSimulation Simulate(AILearningLearner learnerKind)
    {
        var dataSet = AILearningRestaurantData.Create();
        ILearner learner = learnerKind == AILearningLearner.DecisionTree
            ? new DecisionTreeLearner()
            : new MajorityLearner();

        learner.Train(dataSet);
        var results = learner.Test(dataSet);
        var attributeNames = dataSet.GetNonTargetAttributes().ToList();
        var gains = attributeNames
            .Select(attribute => new
            {
                Attribute = attribute,
                Gain = dataSet.CalculateGainFor(attribute)
            })
            .OrderByDescending(entry => entry.Gain)
            .ToList();

        var steps = dataSet.Examples
            .Select((example, index) => new AILearningStep(
                index + 1,
                FormatExample(example, attributeNames),
                example.TargetValue(),
                learner.Predict(example),
                string.Equals(example.TargetValue(), learner.Predict(example), StringComparison.Ordinal)))
            .ToList();

        return new AILearningSimulation(
            learnerKind == AILearningLearner.DecisionTree ? "Decision Tree" : "Majority Learner",
            "Restaurant",
            dataSet.Examples.Count,
            results[0],
            results[1],
            DecisionTree.GetStumpsFor(dataSet, "Yes", "Unable to classify").Count(),
            gains.First().Attribute,
            gains.Select(entry => $"{entry.Attribute}={entry.Gain:F3}").ToList(),
            steps);
    }

    private static string FormatExample(IExample example, IEnumerable<string> attributes)
    {
        return string.Join(", ", attributes.Select(attribute => $"{attribute}={example.GetAttributeValueAsString(attribute)}"));
    }
}