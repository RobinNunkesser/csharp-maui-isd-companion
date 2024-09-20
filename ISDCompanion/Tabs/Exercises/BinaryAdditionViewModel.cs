using Italbytz.Adapters.Exam.ComputingSystems;

namespace StudyCompanion;

public class BinaryAdditionViewModel : ExerciseViewModel
{
    private string _summand1;
    public string Summand1
    {
        get => _summand1;
        set
        {
            if (value == _summand1) return;
            _summand1 = value;
            OnPropertyChanged();
        }
    }
    
    private string _summand2;
    public string Summand2
    {
        get => _summand2;
        set
        {
            if (value == _summand2) return;
            _summand2 = value;
            OnPropertyChanged();
        }
    }
    
    private string _sum;
    public string Sum
    {
        get => _sum;
        set
        {
            if (value == _sum) return;
            _sum = value;
            OnPropertyChanged();
        }
    }
    
    protected override void newExercise()
    {
        var parameters = new BinaryAdditionParameters();
        var solver = new BinaryAdditionSolver();
        var solution = solver.Solve(parameters);

        Summand1 = Convert.ToString(parameters.Summand1,2).PadLeft(9, ' ');;
        Summand2 = Convert.ToString(parameters.Summand2,2).PadLeft(9, ' ');
        Sum = Convert.ToString(solution.Sum,2).PadLeft(9, ' ');;
    }
}