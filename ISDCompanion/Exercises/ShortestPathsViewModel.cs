using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Italbytz.Adapters.Exam.Networks;
using Italbytz.Ports.Exam.Networks;
using Xamarin.Forms;
using ISDCompanion.Exercises.Baseclasses;
using GraphGen.Classes;

namespace ISDCompanion
{
    public class ShortestPathsViewModel : ExerciseViewModel
    {


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


        protected SimpleGraphGen _GraphGen = null;
        public SimpleGraphGen GraphGen
        {
            get { return _GraphGen; }
            set { _GraphGen = value; }
        }
        protected ShortestPathsParameters ShortestPathsParameters { get; set; }
        protected ShortestPathsSolver ShortestPathsSolver { get; set; }

        protected int CurrentSolutionStep { get; set; }
        protected IShortestPathsSolution ShortestPathsSolution { get; set; }

        protected override void newExercise()
        {
            try
            {
                List<string> nodes = new List<string>();
                List<string[]> edges = new List<string[]>();


                CurrentSolutionStep = 0;
                ShortestPathsParameters = new ShortestPathsParameters();
                ShortestPathsSolver = new ShortestPathsSolver();
                ShortestPathsSolution = ShortestPathsSolver.Solve(ShortestPathsParameters);

                foreach (var edge in ShortestPathsParameters.Graph.Edges)
                {
                    if (!nodes.Contains(edge.Source))
                    {
                        nodes.Add(edge.Source);
                    }

                    if (!nodes.Contains(edge.Target))
                    {
                        nodes.Add(edge.Target);
                    }
                    var tmpEdge = new string[3]
                    {
                    edge.Source,
                    edge.Tag.ToString(),
                    edge.Target,
                    };
                    edges.Add(tmpEdge);

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
                    GraphGen.GetNode(edge[0]).AddEdgeTo(GraphGen.GetNode(edge[2]), edge[1]);
                }

                Exercise_Content = GraphGen.RenderLayout();
            }
            catch (Exception e)
            {

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
        private void nextStep()
        {
            if (CurrentSolutionStep <= ShortestPathsSolution.Paths.Count - 1)
            {
                var values = GetCurrentStepInfos(CurrentSolutionStep);
                foreach (var value in values.Values)
                {
                    var pathValues = value.Value;
                    GraphGen.GetNode(pathValues[0]).GetEdgeTo(GraphGen.GetNode(pathValues[2])).Mark();
                }

                CurrentSolutionStep++;
            }
        }

        private PathSolution GetCurrentStepInfos(int stepcounter)
        {
            //catching exceptions
            if (!(stepcounter <= ShortestPathsSolution.Paths.Count - 1 && stepcounter >= 0))
            {
                return null;
            }

            var step = ShortestPathsSolution.Paths[stepcounter];
            var returnValue = new Dictionary<int, string[]>();

            Regex rx = new Regex(@"([a-zA-Z])(.*?)([a-zA-Z]).*?([\d]+)", RegexOptions.Compiled | RegexOptions.Multiline);
            MatchCollection matches = rx.Matches(step);

            int i = 0;
            foreach (Match match in matches)
            {
                GroupCollection groups = match.Groups;

                returnValue.Add(i, new string[3]);

                returnValue[i][0] = groups[1].Value;//Source
                returnValue[i][1] = groups[4].Value.ToString();//Tag
                returnValue[i][2] = groups[3].Value;//Target
                i++;
            }

            return new PathSolution(returnValue);
        }

        private void lastStep()
        {
            CurrentSolutionStep--;
            if (CurrentSolutionStep >= 0)
            {
                var values = GetCurrentStepInfos(CurrentSolutionStep);
                foreach (var value in values.Values)
                {
                    var pathValues = value.Value;
                    GraphGen.GetNode(pathValues[0]).GetEdgeTo(GraphGen.GetNode(pathValues[2])).UnMark();
                }
            }
            else
            {
                CurrentSolutionStep = 0;
            }
        }

        private void showCompleteSolution()
        {
            for (int i = 0; i < ShortestPathsSolution.Paths.Count; i++)
            {
                nextStep();
            }
        }

        private void showInfo()
        {
            if (CurrentSolutionStep > 0)
            {
                string InfoText = "";
                //last step infos
                var values = GetCurrentStepInfos(CurrentSolutionStep - 1);
                InfoText += "Der kürzeste Weg zu '" + values.First + "' ausgehend von ";
                InfoText += "'" + values.Last + "' ist zu erreichen über: \n";

                foreach (var value in values.Values)
                {
                    var pathValues = value.Value;
                    InfoText += pathValues[0] + " ->(" + pathValues[1] + ") " + pathValues[2] + "\n";
                }

                App.Current.MainPage.DisplayAlert("Hinweis", InfoText, "Ok");
            }
        }

    }
}