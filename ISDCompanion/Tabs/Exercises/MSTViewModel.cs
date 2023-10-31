using Italbytz.Adapters.Exam.Networks;
using Italbytz.Ports.Graph;
using StudyCompanion.Exercises.Extensions;
using StudyCompanion.Services.InfoTextServices;
using Italbytz.Maui.Graphics;
using Italbytz.Adapters.Exam.Networks.Graph;
using Italbytz.Ports.Exam.Networks;

namespace StudyCompanion
{
    public class MSTViewModel : StepwiseExerciseViewModel
    {
        protected MinimumSpanningTreeParameters MinimumSpanningTreeParameters { get; set; }
        protected MinimumSpanningTreeSolver MinimumSpanningTreeSolver { get; set; }

        protected IMinimumSpanningTreeSolution MSTVSolution { get; set; }

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

        protected override void newExercise()
        {
            CurrentSolutionStep = 0;

            MinimumSpanningTreeParameters = new MinimumSpanningTreeParameters();
            MinimumSpanningTreeSolver = new MinimumSpanningTreeSolver();
            MSTVSolution = MinimumSpanningTreeSolver.Solve(MinimumSpanningTreeParameters);

            Exercise_Content = new GraphicsView()
            {
                Drawable = new GraphDrawable(MinimumSpanningTreeParameters.Graph, (edge) => false)
            };

        }

        protected override void nextStep()
        {
            if (CurrentSolutionStep < MSTVSolution.Edges.Count())
            {
                var markedEdges = MSTVSolution.Edges.Take(++CurrentSolutionStep);

                Func<ITaggedEdge<string, double>, bool> mark = (edge) => markedEdges.Contains(edge);

                Exercise_Content = new GraphicsView()
                {
                    Drawable = new GraphDrawable(MinimumSpanningTreeParameters.Graph, mark)
                };



            }
        }


        protected override void previousStep()
        {
            if (CurrentSolutionStep > 0)
            {
                CurrentSolutionStep--;
                Func<ITaggedEdge<string, double>, bool> mark = (edge) => false;
                if (CurrentSolutionStep > 0)
                {
                    var markedEdges = MSTVSolution.Edges.Take(CurrentSolutionStep);
                    mark = (edge) => markedEdges.Contains(edge);
                }

                Exercise_Content = new GraphicsView()
                {
                    Drawable = new GraphDrawable(MinimumSpanningTreeParameters.Graph, mark)
                };
            }

        }

        protected override void showCompleteSolution()
        {
            Func<ITaggedEdge<string, double>, bool> mark = (edge) => MSTVSolution.Edges.Contains(edge);
            CurrentSolutionStep = MSTVSolution.Edges.Count();

            Exercise_Content = new GraphicsView()
            {
                Drawable = new GraphDrawable(MinimumSpanningTreeParameters.Graph, mark)
            };

        }

        protected override void showInfo()
        {
            /*if (CurrentSolutionStep > 0)
            {
                string InfoText = MSTV_InfoTextService.GetInfoText(GetCurrentStepInfos(CurrentSolutionStep - 1));
                App.Current.MainPage.DisplayAlert("Info", InfoText, "Ok");
            }*/
        }
    }
}
