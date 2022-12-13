using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ISDCompanion.Services;
using Italbytz.Adapters.Exam.OperatingSystems;

namespace ISDCompanion
{
    public class RealtimeSchedulingViewModel : ExerciseViewModel
    {
        private RealtimeSchedulingStepViewModel[] steps = new RealtimeSchedulingStepViewModel[32];
        private RealtimeSchedulingStepViewModel[] items = new RealtimeSchedulingStepViewModel[32];
        public RealtimeSchedulingStepViewModel[] Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
                OnPropertyChanged();
            }
        }

        protected int CurrentSolutionStep { get; set; }

        protected override void Initialize()
        {
            ButtonNewExercise = new Command(newExercise, () => true);
            ButtonNextStep = new Command(nextStep, () => true);
            ButtonLastStep = new Command(lastStep, () => true);
            ButtonCompleteSolution = new Command(showCompleteSolution, () => true);
            ButtonInfo = new Command(showInfoAsync, () => true);
            newExercise();
        }

        private void showInfoAsync()
        {
            throw new NotImplementedException();
        }

        private void showCompleteSolution()
        {
            throw new NotImplementedException();
        }

        private void lastStep()
        {
            throw new NotImplementedException();
        }

        private void nextStep()
        {
            if (CurrentSolutionStep == 32) return;
            CurrentSolutionStep++;
            setItemsForCurrentStep();

        }

        private void setItemsForCurrentStep()
        {
            Items = (RealtimeSchedulingStepViewModel[])steps.Select((item) => new RealtimeSchedulingStepViewModel()
            {
                A = item.A,
                B = item.B,
                C = item.C,
                EDF = item.Step < CurrentSolutionStep ? item.EDF : Colors.Transparent,
                RMS = item.Step < CurrentSolutionStep ? item.RMS : Colors.Transparent,
            });
        }

        protected async override void newExercise()
        {
            CurrentSolutionStep = 0;

            var parameters = new RealtimeSchedulingParameters();
            var edfSolution = new EDFSolver().Solve(parameters);
            var rmsSolution = new RMSSolver().Solve(parameters);
            for (int i = 0; i < 32; i++)
            {
                steps[i] = new RealtimeSchedulingStepViewModel()
                {
                    Step = i
                };
                steps[i].A = (i % parameters.Requests[0].Item2 < parameters.Requests[0].Item1) ? Colors.Red : Colors.Transparent;
                steps[i].B = (i % parameters.Requests[1].Item2 < parameters.Requests[1].Item1) ? Colors.Blue : Colors.Transparent;
                steps[i].C = (i % parameters.Requests[2].Item2 < parameters.Requests[2].Item1) ? Colors.Green : Colors.Transparent;
                switch (edfSolution.Processes[i])
                {
                    case 0: steps[i].EDF = Colors.Red; break;
                    case 1: steps[i].EDF = Colors.Blue; break;
                    case 2: steps[i].EDF = Colors.Green; break;
                    default:
                        steps[i].EDF = Colors.Transparent; break;
                }
                switch (rmsSolution.Processes[i])
                {
                    case 0: steps[i].RMS = Colors.Red; break;
                    case 1: steps[i].RMS = Colors.Blue; break;
                    case 2: steps[i].RMS = Colors.Green; break;
                    default:
                        steps[i].RMS = Colors.Transparent; break;
                }
            }
            Items = steps;
            //setItemsForCurrentStep();

        }
    }
}
