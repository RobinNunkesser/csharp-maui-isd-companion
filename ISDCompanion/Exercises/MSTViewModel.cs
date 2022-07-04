using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ISDCompanion.Exercises.Baseclasses;
using ISDCompanion.Exercises.Extensions;
using Italbytz.Adapters.Exam.Networks;
using Italbytz.Ports.Exam.Networks;
using Xamarin.Forms;
using System.Linq;
using System.Text.RegularExpressions;

namespace ISDCompanion
{
    public class MSTViewModel : ExerciseViewModel
    {
        protected MinimumSpanningTreeParameters MinimumSpanningTreeParameters { get; set; }
        protected MinimumSpanningTreeSolver MinimumSpanningTreeSolver { get; set; }

        protected IMinimumSpanningTreeSolution MSTVSolution { get; set; }

        protected int CurrentSolutionStep { get; set; }
        private View _GraphContent = null;
        public View GraphContent
        {
            get
            {
                return _GraphContent;
            }
            set
            {
                _GraphContent = value;
                OnPropertyChanged();
            }
        }
        public int ContentHeight { get { return 300; } }
        public int ContentWidth { get { return 900; } }


        protected GraphGen.Classes.GraphGen _GraphGen = null;
        public GraphGen.Classes.GraphGen GraphGen
        {
            get { return _GraphGen; }
            set { _GraphGen = value; }
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
            List<string> nodes = new List<string>();
            GraphGen = new GraphGen.Classes.GraphGen(ContentWidth, ContentHeight);

            CurrentSolutionStep = 0;

            MinimumSpanningTreeParameters = new MinimumSpanningTreeParameters();
            MinimumSpanningTreeSolver = new MinimumSpanningTreeSolver();
            MSTVSolution = MinimumSpanningTreeSolver.Solve(MinimumSpanningTreeParameters);

            foreach (var edge in MinimumSpanningTreeParameters.Graph.Edges)
            {
                if (!nodes.Contains(edge.Source))
                {
                    GraphGen.AddNewNode(edge.Source);
                    nodes.Add(edge.Source);
                }


                if (!nodes.Contains(edge.Target))
                {
                    GraphGen.AddNewNode(edge.Target);
                    nodes.Add(edge.Target);
                }

                GraphGen.GetNode(edge.Source).AddEdgeTo(GraphGen.GetNode(edge.Target), edge.Tag.ToString());
            }

            GraphContent = GraphGen.RenderLayout();
        }
        private void nextStep()
        {
            if (CurrentSolutionStep <= MSTVSolution.Count() - 1)
            {
                var value = GetCurrentStepInfos(CurrentSolutionStep);
                GraphGen.GetNode(value.Source).GetEdgeTo(GraphGen.GetNode(value.Target)).Mark();
                CurrentSolutionStep++;
            }
        }

        private ITaggedEdge<string, double> GetCurrentStepInfos(int stepcounter)
        {
            //catching exceptions
            if (!(stepcounter <= MSTVSolution.Count() - 1 && stepcounter >= 0))
            {
                return null;
            }

            var step = MSTVSolution.GetByIndex(stepcounter);
            return step;

        }

        private void lastStep()
        {
            CurrentSolutionStep--;
            if (CurrentSolutionStep >= 0)
            {
                var value = GetCurrentStepInfos(CurrentSolutionStep);
                GraphGen.GetNode(value.Source).GetEdgeTo(GraphGen.GetNode(value.Target)).UnMark();
            }
            else
            {
                CurrentSolutionStep = 0;
            }
        }

        private void showCompleteSolution()
        {
            var test = MSTVSolution.Count();
            for (int i = 0; i < MSTVSolution.Count(); i++)
            {
                nextStep();
            }
        }

        private void showInfo()
        {
            if (CurrentSolutionStep > 0)
            {
                string InfoText = "";

                var values = GetCurrentStepInfos(CurrentSolutionStep - 1);
                InfoText += "Der kürzeste Weg um '" + values.Source + "' mit \n";
                InfoText += " '" + values.Target + "' zu Verbinden ist über die \n";
                InfoText += "Kante mit der Gewichtung '" + values.Tag + "'.\n";

                App.Current.MainPage.DisplayAlert("Hinweis", InfoText, "Ok");
            }
        }



    }


}
