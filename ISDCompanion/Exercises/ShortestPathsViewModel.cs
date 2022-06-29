using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Italbytz.Adapters.Exam.Networks;
using Italbytz.Ports.Exam.Networks;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class ShortestPathsViewModel : ExerciseViewModel
    {
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
        protected ShortestPathsParameters ShortestPathsParameters { get; set; }
        protected ShortestPathsSolver ShortestPathsSolver { get; set; }

        protected int CurrentSolutionStep { get; set; }
        protected IShortestPathsSolution ShortestPathsSolution { get; set; }


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
                var step = ShortestPathsSolution.Paths[CurrentSolutionStep];

                Regex rx = new Regex(@"([a-zA-Z])(.*?)([a-zA-Z])", RegexOptions.Compiled | RegexOptions.Multiline);
                MatchCollection matches = rx.Matches(step);

                foreach(Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    GraphGen.GetNode(groups[1].Value).GetEdgeTo(GraphGen.GetNode(groups[3].Value)).Mark();
                }

                CurrentSolutionStep++;
            }
        }

        private void lastStep()
        {
            CurrentSolutionStep--;
            if(CurrentSolutionStep >= 0)
            {
                var step = ShortestPathsSolution.Paths[CurrentSolutionStep];

                Regex rx = new Regex(@"([a-zA-Z])(.*?)([a-zA-Z])", RegexOptions.Compiled | RegexOptions.Multiline);
                MatchCollection matches = rx.Matches(step);

                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    GraphGen.GetNode(groups[1].Value).GetEdgeTo(GraphGen.GetNode(groups[3].Value)).UnMark();
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

        }
        protected override void newExercise()
        {
            List<string> nodes = new List<string>();
            GraphGen = new GraphGen.Classes.GraphGen(ContentWidth, ContentHeight);

            CurrentSolutionStep = 0;
            ShortestPathsParameters = new ShortestPathsParameters();
            ShortestPathsSolver = new ShortestPathsSolver();
            ShortestPathsSolution = ShortestPathsSolver.Solve(ShortestPathsParameters);

            foreach (var edge in ShortestPathsParameters.Graph.Edges)
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

    }


}



