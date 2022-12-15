using System.Windows.Input;

namespace ISDCompanion
{
    public abstract class ExerciseViewModel : ViewModel
    {
        public ICommand NewParams { get; set; }

        protected abstract void newExercise();

        public ExerciseViewModel()
        {
            NewParams = new Command(Initialize);
        }

        public virtual void Initialize() => Task.Run(newExercise);
    }
}
