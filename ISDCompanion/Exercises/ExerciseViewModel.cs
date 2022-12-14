using System.Windows.Input;

namespace ISDCompanion
{
    public abstract class ExerciseViewModel : ViewModel
    {
        public ICommand NewParams { get; set; }

        protected abstract void newExercise();

        public ExerciseViewModel()
        {
            Initialize();
            NewParams = new Command(Initialize);
        }

        protected void Initialize() => newExercise();
    }
}
