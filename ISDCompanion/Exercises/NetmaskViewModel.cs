using Italbytz.Adapters.Exam.Networks;

namespace ISDCompanion
{
    public class NetmaskViewModel : ExerciseViewModel
    {
        private string address;
        public string Address
        {
            get => address;
            set
            {
                if (value != address)
                {
                    address = value;
                    OnPropertyChanged();
                }
            }
        }

        private string network;
        public string Network
        {
            get => network;
            set
            {
                if (value != network)
                {
                    network = value;
                    OnPropertyChanged();
                }
            }
        }

        private string host;
        public string Host
        {
            get => host;
            set
            {
                if (value != host)
                {
                    host = value;
                    OnPropertyChanged();
                }
            }
        }

        protected override void newExercise()
        {
            var parameters = new NetmaskParameters();
            var solver = new NetmaskSolver();
            var solution = solver.Solve(parameters);

            Address = parameters.Address + "/" + parameters.PrefixLength;
            Network = solution.NetworkAddress.ToString();
            Host = solution.HostAddress.ToString();
        }

    }
}
