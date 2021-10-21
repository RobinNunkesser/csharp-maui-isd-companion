using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Italbytz.Adapters.Exam.OperatingSystems;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class BuddyViewModel : ExerciseViewModel
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

        private string freeOrder;
        public string FreeOrder
        {
            get => freeOrder;
            set
            {
                if (value != freeOrder)
                {
                    freeOrder = value;
                    OnPropertyChanged();
                }
            }
        }

        private string solution;
        public string Solution
        {
            get => solution;
            set
            {
                if (value != solution)
                {
                    solution = value;
                    OnPropertyChanged();
                }
            }
        }

        protected override void Initialize()
        {
            var parameters = new BuddyParameters();
            var solver = new BuddySolver();
            var solution = solver.Solve(parameters);

            var solutionString = "";

            foreach (var entry in solution.History)
            {
                foreach (var cell in entry)
                {
                    solutionString += cell == -1 ? "-" : parameters.Processes[cell];
                }
                solutionString += "\n";
            }

            Requests = $"A ({parameters.Requests[0]}), B ({parameters.Requests[1]}), C ({parameters.Requests[2]}), D ({parameters.Requests[3]}), E ({parameters.Requests[4]})";
            FreeOrder = $"Free {parameters.FreeOrder[0]}, {parameters.FreeOrder[1]}, {parameters.FreeOrder[2]}, {parameters.FreeOrder[3]}, {parameters.FreeOrder[4]}";
            Solution = solutionString;
        }

    }
}
