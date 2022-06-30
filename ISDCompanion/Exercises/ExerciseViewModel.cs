using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ISDCompanion
{
    public abstract class ExerciseViewModel : ViewModel
    {
        public ICommand NewParams { get; set; }

        public ICommand ButtonNextStep { set; get; }
        public ICommand ButtonLastStep { set; get; }
        public ICommand ButtonCompleteSolution { set; get; }
        public ICommand ButtonInfo { set; get; }
        public ICommand ButtonNewExercise { set; get; }

        protected abstract void newExercise();
        public ExerciseViewModel()
        {
            Initialize();
            NewParams = new Command(Initialize);
        }

        protected abstract void Initialize();
    }
}

