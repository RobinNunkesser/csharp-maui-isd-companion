using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Italbytz.Adapters.Exam.Networks;
using Italbytz.Maui.Graphics;

namespace StudyCompanion
{
    public class ShortestPathsViewModel : ExerciseViewModel
    {
        private GraphicsView? _GraphContent = null;
        public View? Exercise_Content
        {
            get
            {
                return _GraphContent;
            }
            set
            {
                _GraphContent = (GraphicsView?)value;
                OnPropertyChanged();
            }
        }

        private string[] solution = new string[7];
        public string[] Solution
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

        protected override void newExercise()
        {
            var parameters = new ShortestPathsParameters();
            var solver = new ShortestPathsSolver();
            var solution = solver.Solve(parameters);

            Exercise_Content = new GraphicsView()
            {
                Drawable = new GraphDrawable(parameters.Graph, (edge) => false)
            };

            Solution = solution.Paths.ToArray();
        }
    }


}



