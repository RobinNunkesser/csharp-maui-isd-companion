using System.Windows.Input;

namespace StudyCompanion
{
    public abstract class StepwiseExerciseViewModel : ExerciseViewModel
    {
        private readonly Command _buttonNextStep;
        private readonly Command _buttonPreviousStep;
        private readonly Command _buttonCompleteSolution;
        private readonly Command _buttonInfo;
        private readonly Command _buttonNewExercise;

        public ICommand ButtonNextStep => _buttonNextStep;
        public ICommand ButtonPreviousStep => _buttonPreviousStep;
        public ICommand ButtonCompleteSolution => _buttonCompleteSolution;
        public ICommand ButtonInfo => _buttonInfo;
        public ICommand ButtonNewExercise => _buttonNewExercise;

        private int _currentSolutionStep;
        protected int CurrentSolutionStep
        {
            get => _currentSolutionStep;
            set
            {
                if (_currentSolutionStep == value) return;
                _currentSolutionStep = value;
                OnPropertyChanged();
                RefreshCommandStates();
            }
        }

        public StepwiseExerciseViewModel() : base()
        {
            _buttonNewExercise = new Command(Initialize);
            _buttonNextStep = new Command(ExecuteNextStep, CanMoveToNextStep);
            _buttonPreviousStep = new Command(ExecutePreviousStep, CanMoveToPreviousStep);
            _buttonCompleteSolution = new Command(ExecuteShowCompleteSolution, CanShowCompleteSolution);
            _buttonInfo = new Command(ExecuteShowInfo, CanShowInfo);
        }

        protected virtual bool CanMoveToNextStep() => true;
        protected virtual bool CanMoveToPreviousStep() => true;
        protected virtual bool CanShowCompleteSolution() => true;
        protected virtual bool CanShowInfo() => true;

        protected void RefreshCommandStates()
        {
            _buttonNextStep.ChangeCanExecute();
            _buttonPreviousStep.ChangeCanExecute();
            _buttonCompleteSolution.ChangeCanExecute();
            _buttonInfo.ChangeCanExecute();
        }

        private void ExecuteNextStep()
        {
            nextStep();
            RefreshCommandStates();
        }

        private void ExecutePreviousStep()
        {
            previousStep();
            RefreshCommandStates();
        }

        private void ExecuteShowCompleteSolution()
        {
            showCompleteSolution();
            RefreshCommandStates();
        }

        private void ExecuteShowInfo()
        {
            showInfo();
            RefreshCommandStates();
        }

        protected abstract void showCompleteSolution();
        protected abstract void previousStep();
        protected abstract void nextStep();
        protected abstract void showInfo();

    }
}



