using ISDCompanion.Services;
using Italbytz.Adapters.Exam.OperatingSystems;

namespace ISDCompanion
{
    public class BuddyViewModel : StepCollectionViewModel<BuddyStepViewModel>
    {
        protected override int NoOfSteps => 10;

        private string request1 { get; set; } = String.Empty;
        public string Request1
        {
            get
            {
                return request1;
            }
            set
            {
                request1 = value;
                OnPropertyChanged();
            }
        }

        private string request2 { get; set; } = String.Empty;
        public string Request2
        {
            get
            {
                return request2;
            }
            set
            {
                request2 = value;
                OnPropertyChanged();
            }
        }
        private string request3 { get; set; } = String.Empty;
        public string Request3
        {
            get
            {
                return request3;
            }
            set
            {
                request3 = value;
                OnPropertyChanged();
            }
        }
        private string request4 { get; set; } = String.Empty;
        public string Request4
        {
            get
            {
                return request4;
            }
            set
            {
                request4 = value;
                OnPropertyChanged();
            }
        }
        private string request5 { get; set; } = String.Empty;
        public string Request5
        {
            get
            {
                return request5;
            }
            set
            {
                request5 = value;
                OnPropertyChanged();
            }
        }

        private string free1 { get; set; } = String.Empty;
        public string Free1
        {
            get
            {
                return free1;
            }
            set
            {
                free1 = value;
                OnPropertyChanged();
            }
        }

        private string free2 { get; set; } = String.Empty;
        public string Free2
        {
            get
            {
                return free2;
            }
            set
            {
                free2 = value;
                OnPropertyChanged();
            }
        }

        private string free3 { get; set; } = String.Empty;
        public string Free3
        {
            get
            {
                return free3;
            }
            set
            {
                free3 = value;
                OnPropertyChanged();
            }
        }

        private string free4 { get; set; } = String.Empty;
        public string Free4
        {
            get
            {
                return free4;
            }
            set
            {
                free4 = value;
                OnPropertyChanged();
            }
        }

        private string free5 { get; set; } = String.Empty;
        public string Free5
        {
            get
            {
                return free5;
            }
            set
            {
                free5 = value;
                OnPropertyChanged();
            }
        }

        protected override void newExercise()
        {
            CurrentSolutionStep = 0;

            var parameters = new BuddyParameters();
            var solver = new BuddySolver();
            var solution = solver.Solve(parameters);

            Request1 = $"{parameters.Processes[0]} ({parameters.Requests[0]})";
            Request2 = $"{parameters.Processes[1]} ({parameters.Requests[1]})";
            Request3 = $"{parameters.Processes[2]} ({parameters.Requests[2]})";
            Request4 = $"{parameters.Processes[3]} ({parameters.Requests[3]})";
            Request5 = $"{parameters.Processes[4]} ({parameters.Requests[4]})";
            Free1 = $"Free {parameters.FreeOrder[0]}";
            Free2 = $"Free {parameters.FreeOrder[1]}";
            Free3 = $"Free {parameters.FreeOrder[2]}";
            Free4 = $"Free {parameters.FreeOrder[3]}";
            Free5 = $"Free {parameters.FreeOrder[4]}";

            var newSteps = solution.History.Select((entry) => new BuddyStepViewModel()
            {
                Occupation = entry.Select((element) => $"{element}").ToArray()
            }).ToArray();

            Steps = newSteps;
        }

        protected override void showInfo()
        {
            throw new NotImplementedException();
        }
    }
}
