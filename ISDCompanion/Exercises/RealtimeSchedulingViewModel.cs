using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using OperatingSystemsExam.Scheduling.Realtime;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class RealtimeSchedulingViewModel : INotifyPropertyChanged
    {
        public ICommand NewParams { get; set; }

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

        public RealtimeSchedulingViewModel()
        {
            Initialize();
            NewParams = new Command(Initialize);
        }

        private void Initialize()
        {
            var parameters = new SchedulingParameters();

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

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion

    }
}
