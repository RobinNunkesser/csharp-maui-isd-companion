using System;
using System.Windows.Input;

namespace ISDCompanion
{
    public abstract class StepwiseExerciseViewModel : ExerciseViewModel
    {
        public ICommand ButtonNextStep { set; get; }
        public ICommand ButtonPreviousStep { set; get; }
        public ICommand ButtonCompleteSolution { set; get; }
        public ICommand ButtonInfo { set; get; }
        public ICommand ButtonNewExercise { set; get; }

        protected int CurrentSolutionStep { get; set; }

        public StepwiseExerciseViewModel() : base()
        {
            ButtonNewExercise = new Command(newExercise, () => true);
            ButtonNextStep = new Command(nextStep, () => true);
            ButtonPreviousStep = new Command(previousStep, () => true);
            ButtonCompleteSolution = new Command(showCompleteSolution, () => true);
            ButtonInfo = new Command(showInfo, () => true);
        }

        protected abstract void showCompleteSolution();
        protected abstract void previousStep();
        protected abstract void nextStep();
        protected abstract void showInfo();

    }
}



