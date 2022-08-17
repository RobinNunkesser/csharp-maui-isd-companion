using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Italbytz.Adapters.Exam.Networks;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class NetmaskViewModel : ExerciseViewModel
    {
        protected override void newExercise()
        {
            //todo  
        }
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

        protected override void Initialize()
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
