using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Italbytz.Adapters.Exam.Networks;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class BitencodingsViewModel : ExerciseViewModel
    {
        private string bits;
        public string Bits
        {
            get => bits;
            set
            {
                if (value != bits)
                {
                    bits = value;
                    OnPropertyChanged();
                }
            }
        }

        private string nrz;
        public string NRZ
        {
            get => nrz;
            set
            {
                if (value != nrz)
                {
                    nrz = value;
                    OnPropertyChanged();
                }
            }
        }

        private string nrzi;
        public string NRZI
        {
            get => nrzi;
            set
            {
                if (value != nrzi)
                {
                    nrzi = value;
                    OnPropertyChanged();
                }
            }
        }

        private string mlt3;
        public string MLT3
        {
            get => mlt3;
            set
            {
                if (value != mlt3)
                {
                    mlt3 = value;
                    OnPropertyChanged();
                }
            }
        }

        protected override void Initialize()
        {
            var parameters = new BitencodingParameters();
            var solver = new BitencodingSolver();
            var solution = solver.Solve(parameters);
            Bits = string.Join("", parameters.Bits);
            NRZ = string.Join("", solution.NRZ);
            NRZI = string.Join("", solution.NRZI);
            MLT3 = string.Join("", solution.MLT3);
        }

    }
}