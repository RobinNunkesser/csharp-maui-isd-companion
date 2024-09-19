using Italbytz.Adapters.Exam.ComputingSystems;

namespace StudyCompanion;

public class DecimalToBinaryViewModel: ExerciseViewModel
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
        var parameters = new DecimalToBinaryParameters();
        var solver = new DecimalToBinarySolver();
        var solution = solver.Solve(parameters);

        Decimal = Convert.ToString(parameters.Decimal,10);
        Binary = Convert.ToString(solution.Binary,2);
    }
}