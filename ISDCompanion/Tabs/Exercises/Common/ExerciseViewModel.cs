using Microsoft.Maui.ApplicationModel;
using System.Windows.Input;

namespace StudyCompanion
{
    public abstract class ExerciseViewModel : ViewModel
    {
        public ICommand NewParams { get; set; }

        protected abstract void newExercise();

        public ExerciseViewModel()
        {
            NewParams = new Command(Initialize);
        }

        public virtual void Initialize()
        {
            if (MainThread.IsMainThread)
            {
                newExercise();
                return;
            }

            MainThread.BeginInvokeOnMainThread(newExercise);
        }
    }
}
