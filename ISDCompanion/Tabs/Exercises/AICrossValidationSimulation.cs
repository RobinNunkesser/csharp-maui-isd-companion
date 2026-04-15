using Italbytz.AI.Learning;
using Italbytz.AI.Learning.Inductive;

namespace StudyCompanion;

internal sealed record AICrossValidationStep(
    int ParameterSize,
    double TrainingError,
    double ValidationError,
    bool Selected);

internal sealed record AICrossValidationSimulation(
    string DatasetName,
    string LearnerName,
    int FoldCount,
    double Threshold,
    int SelectedParameterSize,
    IReadOnlyList<AICrossValidationStep> Steps);

internal sealed class AICrossValidationSimulator
{
    private const int IdealParameterSize = 4;

    public AICrossValidationSimulation Simulate()
    {
        const int foldCount = 5;
        const double threshold = 0.05;

        var dataSet = AILearningRestaurantData.Create();
        var learner = new DemoParameterizedLearner();
        var crossValidation = new CrossValidation(threshold);
        var selectedLearner = crossValidation.CrossValidationWrapper(learner, foldCount, dataSet);
        var selectedParameterSize = selectedLearner.ParameterSize;

        var steps = Enumerable.Range(0, selectedParameterSize + 1)
            .Select(size => new AICrossValidationStep(
                size,
                CalculateTrainingError(size),
                CalculateValidationError(size),
                size == selectedParameterSize))
            .ToList();

        return new AICrossValidationSimulation(
            "Restaurant",
            "Parameterized Demo Learner",
            foldCount,
            threshold,
            selectedParameterSize,
            steps);
    }

    private static double CalculateTrainingError(int parameterSize)
    {
        return parameterSize >= IdealParameterSize
            ? 0.04
            : Math.Min(0.04 + ((IdealParameterSize - parameterSize) * 0.2), 0.99);
    }

    private static double CalculateValidationError(int parameterSize)
    {
        return Math.Min(0.04 + (Math.Abs(IdealParameterSize - parameterSize) * 0.16), 0.99);
    }

    private sealed class DemoParameterizedLearner : IParameterizedLearner
    {
        public int ParameterSize { get; set; }

        public void Train(IDataSet ds)
        {
        }

        public void Train(int size, IDataSet dataSet)
        {
            ParameterSize = size;
            Train(dataSet);
        }

        public string[] Predict(IDataSet ds)
        {
            return ds.Examples.Select(Predict).ToArray();
        }

        public string Predict(IExample e)
        {
            return ParameterSize >= IdealParameterSize ? "Yes" : "No";
        }

        public int[] Test(IDataSet ds)
        {
            var error = ds.Examples.Count > 5
                ? CalculateTrainingError(ParameterSize)
                : CalculateValidationError(ParameterSize);

            var delta = (int)Math.Round(error * 100);
            var first = 50 + (delta / 2);
            var second = 50 - (delta / 2);
            return [first, second];
        }
    }
}