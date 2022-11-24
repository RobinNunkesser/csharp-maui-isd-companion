using Italbytz.Adapters.Exam.Networks;
using Italbytz.Ports.Graph;
using ISDCompanion.Exercises.Extensions;
using ISDCompanion.Services.InfoTextServices;
using Italbytz.Maui.Graphics;
using Italbytz.Adapters.Exam.Networks.Graph;
using Italbytz.Ports.Exam.Networks;

namespace ISDCompanion
{
    public class MSTViewModel : ExerciseViewModel
    {
        protected MinimumSpanningTreeParameters MinimumSpanningTreeParameters { get; set; }
        protected MinimumSpanningTreeSolver MinimumSpanningTreeSolver { get; set; }

        protected IMinimumSpanningTreeSolution MSTVSolution { get; set; }

        protected int CurrentSolutionStep { get; set; }

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

        protected override void Initialize()
        {
            ButtonNewExercise = new Command(newExercise, () => true);
            ButtonNextStep = new Command(nextStep, () => true);
            ButtonLastStep = new Command(lastStep, () => true);
            ButtonCompleteSolution = new Command(showCompleteSolution, () => true);
            ButtonInfo = new Command(showInfo, () => true);
            newExercise();
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

        private void nextStep()
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


        private void lastStep()
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

        private void showCompleteSolution()
        {
            Func<ITaggedEdge<string, double>, bool> mark = (edge) => MSTVSolution.Edges.Contains(edge);
            CurrentSolutionStep = MSTVSolution.Edges.Count();

            Exercise_Content = new GraphicsView()
            {
                Drawable = new GraphDrawable(MinimumSpanningTreeParameters.Graph, mark)
            };

        }

        private void showInfo()
        {
            /*if (CurrentSolutionStep > 0)
            {
                string InfoText = MSTV_InfoTextService.GetInfoText(GetCurrentStepInfos(CurrentSolutionStep - 1));
                App.Current.MainPage.DisplayAlert("Info", InfoText, "Ok");
            }*/
        }
    }
}
