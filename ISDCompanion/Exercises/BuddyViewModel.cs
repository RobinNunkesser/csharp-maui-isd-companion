using ISDCompanion.Services;
using Italbytz.Adapters.Exam.OperatingSystems;

namespace ISDCompanion
{
    public class BuddyViewModel : StepCollectionViewModel<BuddyStepViewModel>
    {
        protected override int NoOfSteps => 10;

        protected override void newExercise()
        {
            CurrentSolutionStep = 0;

            var parameters = new BuddyParameters();
            var solver = new BuddySolver();
            var solution = solver.Solve(parameters);

            var newSteps = solution.History.Select((entry) => new BuddyStepViewModel()
            {
                Occupation = entry.Select((element) =>
                {
                    switch (element)
                    {
                        case 0: return "A";
                        case 1: return "B";
                        case 2: return "C";
                        case 3: return "D";
                        case 4: return "E";
                        default:
                            return " ";
                    }
                }).ToArray()
            }).ToArray();

            for (int i = 0; i < 5; i++)
            {
                newSteps[i].Label = $"{parameters.Processes[i]} ({parameters.Requests[i]})";
            }
            for (int i = 5; i < 10; i++)
            {
                newSteps[i].Label = $"Free {parameters.FreeOrder[i - 5]}";
            }

            Steps = newSteps;
        }

        protected override void showInfo()
        {
            throw new NotImplementedException();
        }
    }
}
