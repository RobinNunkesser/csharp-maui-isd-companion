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
using GraphGen.Classes;
using ISDCompanion.Services.InfoTextServices;

namespace ISDCompanion
{
    public class MSTViewModel : ExerciseViewModel
    {
        protected MinimumSpanningTreeParameters MinimumSpanningTreeParameters { get; set; }
        protected MinimumSpanningTreeSolver MinimumSpanningTreeSolver { get; set; }

        protected IMinimumSpanningTreeSolution MSTVSolution { get; set; }

        protected int CurrentSolutionStep { get; set; }
        private View _GraphContent = null;
        public View Exercise_Content
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
        private int _ContentHeight = 0;
        public int ContentHeight
        {
            get { return _ContentHeight; }
            set
            {
                _ContentHeight = value;
                OnPropertyChanged();
            }
        }
        private int _ContentWidth = 0;
        public int ContentWidth
        {
            get { return _ContentWidth; }
            set
            {
                _ContentWidth = value;
                OnPropertyChanged();
            }
        }


        protected GraphGen.Classes.SimpleGraphGen _GraphGen = null;
        public GraphGen.Classes.SimpleGraphGen GraphGen
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
            List<string[]> edges = new List<string[]>();


            CurrentSolutionStep = 0;

            MinimumSpanningTreeParameters = new MinimumSpanningTreeParameters();
            MinimumSpanningTreeSolver = new MinimumSpanningTreeSolver();
            MSTVSolution = MinimumSpanningTreeSolver.Solve(MinimumSpanningTreeParameters);




            foreach (var edge in MinimumSpanningTreeParameters.Graph.Edges)
            {
                if (!nodes.Contains(edge.Source))
                {

                    nodes.Add(edge.Source);
                }


                if (!nodes.Contains(edge.Target))
                {

                    nodes.Add(edge.Target);
                }
                var tmp = new string[3]
                {
                    edge.Source,edge.Tag.ToString(),edge.Target
                };
                edges.Add(tmp);
            }

            GraphGen = new SimpleGraphGen(nodes.Count);
            ContentHeight = SimpleGraphGen.CalcMaxHeight();
            ContentWidth = SimpleGraphGen.CalcMaxWidth(nodes.Count);
            foreach (var node in nodes)
            {
                GraphGen.AddNewNode(node);
            }
            foreach (var edge in edges)
            {
                var s = GraphGen.GetNode(edge[0]);
                var t = GraphGen.GetNode(edge[2]);

                s.AddEdgeTo(t, edge[1]);
            }

            Exercise_Content = GraphGen.RenderLayout();

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
                string InfoText = MSTV_InfoTextService.GetInfoText(GetCurrentStepInfos(CurrentSolutionStep - 1));
                App.Current.MainPage.DisplayAlert("Info", InfoText, "Ok");
            }
        }
    }
}
