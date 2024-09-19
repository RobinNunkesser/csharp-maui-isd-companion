using Italbytz.Adapters.Exam.ComputingSystems;
using Italbytz.Adapters.Exam.Networks;

namespace StudyCompanion;

public class BinaryToDecimalViewModel : ExerciseViewModel
{
    private string _decimal;
    public string Decimal
    {
        get => _decimal;
        set
        {
            if (value != _decimal)
            {
                _decimal = value;
                OnPropertyChanged();
            }
        }
    }
    
    private string _binary;
    public string Binary
    {
        get => _binary;
        set
        {
            if (value != _binary)
            {
                _binary = value;
                OnPropertyChanged();
            }
        }
    }
    
    protected override void newExercise()
    {
        var parameters = new BinaryToDecimalParameters();
        var solver = new BinaryToDecimalSolver();
        var solution = solver.Solve(parameters);

        Binary = Convert.ToString(parameters.Binary,2);
        Decimal = Convert.ToString(solution.Decimal,10);
    }
}