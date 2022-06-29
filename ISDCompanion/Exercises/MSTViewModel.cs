using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Italbytz.Adapters.Exam.Networks;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class MSTViewModel : ExerciseViewModel
    {
        protected override void newExercise()
        {
            //todo  
        }
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

        protected override void Initialize()
        {
            var parameters = new MinimumSpanningTreeParameters();
            var solver = new MinimumSpanningTreeSolver();
            var solution = solver.Solve(parameters);

            var graphString = "";

            foreach (var edge in parameters.Graph.Edges)
            {
                graphString += edge.ToString();
            }

            Graph = graphString;

            var solutionString = "";

            foreach (var edge in solution.Edges)
            {
                solutionString += edge.ToString();
            }

            Solution = solutionString;
        }

    }


}
