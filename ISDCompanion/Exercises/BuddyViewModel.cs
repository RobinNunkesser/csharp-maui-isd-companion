using ISDCompanion.Services;
using Italbytz.Adapters.Exam.OperatingSystems;

namespace ISDCompanion
{
    public class BuddyViewModel //: StepwiseExerciseViewModel<String>
    {
        public void AfterRender()
        {
            var parameters = new BuddyParameters();
            var solver = new BuddySolver();
            var solution = solver.Solve(parameters);



        }

        /*protected override void newExercise()
        {

        }

        protected override void showInfo()
        {
            throw new NotImplementedException();
        }*/
    }
}
