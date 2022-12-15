using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ISDCompanion.Services;
using Italbytz.Adapters.Exam.OperatingSystems;

namespace ISDCompanion
{
    public class RealtimeSchedulingViewModel : StepCollectionViewModel<RealtimeSchedulingStepViewModel>
    {
        protected override int NoOfSteps => 32;

        protected override void newExercise()
        {
            CurrentSolutionStep = 0;

            var parameters = new RealtimeSchedulingParameters();
            var edfSolution = new EDFSolver().Solve(parameters);
            var rmsSolution = new RMSSolver().Solve(parameters);

            var newSteps = new RealtimeSchedulingStepViewModel[NoOfSteps];
            for (int i = 0; i < NoOfSteps; i++)
            {
                newSteps[i] = new RealtimeSchedulingStepViewModel();
                newSteps[i].A = (i % parameters.Requests[0].Item2 < parameters.Requests[0].Item1) ? Colors.Red : Colors.Transparent;
                newSteps[i].B = (i % parameters.Requests[1].Item2 < parameters.Requests[1].Item1) ? Colors.Blue : Colors.Transparent;
                newSteps[i].C = (i % parameters.Requests[2].Item2 < parameters.Requests[2].Item1) ? Colors.Green : Colors.Transparent;
                switch (edfSolution.Processes[i])
                {
                    case 0: newSteps[i].EDF = Colors.Red; break;
                    case 1: newSteps[i].EDF = Colors.Blue; break;
                    case 2: newSteps[i].EDF = Colors.Green; break;
                    default:
                        newSteps[i].EDF = Colors.Transparent; break;
                }
                switch (rmsSolution.Processes[i])
                {
                    case 0: newSteps[i].RMS = Colors.Red; break;
                    case 1: newSteps[i].RMS = Colors.Blue; break;
                    case 2: newSteps[i].RMS = Colors.Green; break;
                    default:
                        newSteps[i].RMS = Colors.Transparent; break;
                }
            }
            Steps = newSteps;

        }

        protected override void showInfo()
        {
            throw new NotImplementedException();
        }
    }
}
