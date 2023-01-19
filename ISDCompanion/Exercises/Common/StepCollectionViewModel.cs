using System;
using System.Windows.Input;

namespace StudyCompanion
{
    public abstract class StepCollectionViewModel<StepType> : StepwiseExerciseViewModel where StepType : ExerciseStepViewModel
    {
        protected abstract int NoOfSteps { get; }

        private StepType[]? steps;
        public StepType[]? Steps
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
            while (CurrentSolutionStep != NoOfSteps)
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
            if (CurrentSolutionStep == NoOfSteps) return;
            showCurrentStep();
            CurrentSolutionStep++;
        }

    }
}



