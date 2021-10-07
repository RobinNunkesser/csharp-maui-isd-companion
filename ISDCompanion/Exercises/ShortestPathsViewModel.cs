using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using NetworksExam.ShortestPaths;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class ShortestPathsViewModel : INotifyPropertyChanged
    {
        public ICommand NewParams { get; set; }

        private string graph = "";
        public string Graph
        {
            get => graph;
            set
            {
                if (value != graph)
                {
                    graph = value;
                    OnPropertyChanged();
                }
            }
        }

        private string solution = "";
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

        public ShortestPathsViewModel()
        {
            Initialize();
            NewParams = new Command(Initialize);
        }

        private void Initialize()
        {
            var parameters = new ShortestPathsParameters();
            var solver = new ShortestPathsSolver();
            var solution = solver.Solve(parameters);

            var graphString = "";

            foreach (var edge in parameters.Graph.Edges)
            {
                graphString += edge.ToString();
            }

            Graph = graphString;

            var solutionString = "";

            foreach (var path in solution.Paths)
            {
                solutionString += path + "\n";
            }

            Solution = solutionString;
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion

    }


}



