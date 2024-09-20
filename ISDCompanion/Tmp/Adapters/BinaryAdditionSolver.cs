using Italbytz.Ports.Exam.ComputingSystems;

namespace Italbytz.Adapters.Exam.ComputingSystems;

public class BinaryAdditionSolver : IBinaryAdditionSolver
{

    public IBinaryAdditionSolution Solve(IBinaryAdditionParameters parameters)
    {
        var solution = new BinaryAdditionSolution
        {
            Sum = (ushort)(parameters.Summand1 + parameters.Summand2)
        };
        return solution;
    }
}