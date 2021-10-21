using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Italbytz.Adapters.Exam.OperatingSystems;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class SchedulingViewModel : INotifyPropertyChanged
    {
        public ICommand NewParams { get; set; }

        private string processes;
        public string Processes
        {
            get => processes;
            set
            {
                if (value != processes)
                {
                    processes = value;
                    OnPropertyChanged();
                }
            }
        }

        private string sjf;
        public string SJF
        {
            get => sjf;
            set
            {
                if (value != sjf)
                {
                    sjf = value;
                    OnPropertyChanged();
                }
            }
        }

        private string fcfs;
        public string FCFS
        {
            get => fcfs;
            set
            {
                if (value != fcfs)
                {
                    fcfs = value;
                    OnPropertyChanged();
                }
            }
        }

        private string prio;
        public string Prio
        {
            get => prio;
            set
            {
                if (value != prio)
                {
                    prio = value;
                    OnPropertyChanged();
                }
            }
        }

        private string rr;
        public string RR
        {
            get => rr;
            set
            {
                if (value != rr)
                {
                    rr = value;
                    OnPropertyChanged();
                }
            }
        }

        public SchedulingViewModel()
        {
            Initialize();
            NewParams = new Command(Initialize);
        }

        private void Initialize()
        {
            var parameters = new SchedulingParameters();
            var processes = "";

            for (int i = 0; i < parameters.Values.Length; i++)
            {
                processes += $"{parameters.Values[i]} ({parameters.Priorities[i]}) ";
            }

            Processes = processes;

            FCFS = $"{new FCFSSolver().Solve(parameters).Time}";
            SJF = $"{new ShortestJobFirstSolver().Solve(parameters).Time}";
            RR = $"{new RoundRobinSolver().Solve(parameters).Time}";
            Prio = $"{new PrioritySchedulingSolver().Solve(parameters).Time}";

        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion
    }
}
