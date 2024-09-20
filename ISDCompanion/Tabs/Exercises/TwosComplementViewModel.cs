using Italbytz.Adapters.Exam.ComputingSystems;

namespace StudyCompanion;

public class TwosComplementViewModel : ExerciseViewModel
{

    private string _positiveBinary;
    public string PositiveBinary
    {
        get => _positiveBinary;
        set
        {
            if (value == _positiveBinary) return;
            _positiveBinary = value;
            OnPropertyChanged();
        }
    }
    
    private string _complementBinary;
    public string ComplementBinary
    {
        get => _complementBinary;
        set
        {
            if (value == _complementBinary) return;
            _complementBinary = value;
            OnPropertyChanged();
        }
    }
    
    protected override void newExercise()
    {
        var parameters = new TwosComplementParameters();
        var solver = new TwosComplementSolver();
        var solution = solver.Solve(parameters);

        PositiveBinary = "-"+Convert.ToString(parameters.PositiveBinary,2).PadLeft(8, '0');;
        ComplementBinary = Convert.ToString(solution.ComplementBinary,2)[8..];
    }
}