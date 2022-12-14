using System;
using System.Windows.Input;

namespace ISDCompanion
{
    public abstract class StepCollectionViewModel<StepType> : StepwiseExerciseViewModel where StepType : ExerciseStepViewModel
    {
        private StepType[] steps = new StepType[32];
        public StepType[] Steps
        {
            get
            {
                return steps;
            }
            set
            {
                steps = value;
                OnPropertyChanged();
            }
        }

        protected override void showCompleteSolution()
        {
            while (CurrentSolutionStep != 32)
            {
                nextStep();
            }
        }

        protected override void previousStep()
        {
            if (CurrentSolutionStep == 0) return;
            CurrentSolutionStep--;
            hideCurrentStep();
        }

        protected void showCurrentStep()
        {
            Steps[CurrentSolutionStep].Visible = true;
        }

        protected void hideCurrentStep()
        {
            Steps[CurrentSolutionStep].Visible = false;
        }


        protected override void nextStep()
        {
            if (CurrentSolutionStep == 32) return;
            showCurrentStep();
            CurrentSolutionStep++;
        }

    }
}



