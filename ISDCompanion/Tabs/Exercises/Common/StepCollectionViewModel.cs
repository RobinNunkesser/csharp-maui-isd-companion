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
                RefreshCommandStates();
            }
        }

        protected override bool CanMoveToNextStep() => Steps != null && CurrentSolutionStep < NoOfSteps;

        protected override bool CanMoveToPreviousStep() => Steps != null && CurrentSolutionStep > 0;

        protected override bool CanShowCompleteSolution() => Steps != null && CurrentSolutionStep < NoOfSteps;

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
            if (Steps == null) return;
            Steps[CurrentSolutionStep].Visible = true;
        }

        protected void hideCurrentStep()
        {
            if (Steps == null) return;
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



