using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Italbytz.Adapters.Exam.OperatingSystems;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class RealtimeSchedulingViewModel : ExerciseViewModel
    {
        private string requests;
        public string Requests
        {
            get => requests;
            set
            {
                if (value != requests)
                {
                    requests = value;
                    OnPropertyChanged();
                }
            }
        }

        private string edfSolution;
        public string EDFSolution
        {
            get => edfSolution;
            set
            {
                if (value != edfSolution)
                {
                    edfSolution = value;
                    OnPropertyChanged();
                }
            }
        }

        private string rmsSolution;
        public string RMSSolution
        {
            get => rmsSolution;
            set
            {
                if (value != rmsSolution)
                {
                    rmsSolution = value;
                    OnPropertyChanged();
                }
            }
        }

        protected override void Initialize()
        {
            var parameters = new RealtimeSchedulingParameters();

            var requests = "";
            var index = 0;
            foreach (var request in parameters.Requests)
            {
                requests += $"Process {index}: (Time: {request.Item1}, Freq: {request.Item2})\n";
                index++;
            }

            Requests = requests;
            EDFSolution = string.Join("", new EDFSolver().Solve(parameters).Processes);
            RMSSolution = string.Join("", new RMSSolver().Solve(parameters).Processes);

        }

    }
}
