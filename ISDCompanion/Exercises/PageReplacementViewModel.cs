using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Italbytz.Adapters.Exam.OperatingSystems;
using Italbytz.Infrastructure.Exam.OperatingSystems.PageReplacement;
using Italbytz.Ports.Exam.OperatingSystems;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class PageReplacementViewModel : ExerciseViewModel
    {

        public List<string[]> Items { get; set; }
        private string referenceRequests;
        public string ReferenceRequests
        {
            get => referenceRequests;
            set
            {
                if (value != referenceRequests)
                {
                    referenceRequests = value;
                    OnPropertyChanged();
                }
            }
        }

        private int selectedStrategy;
        public int SelectedStrategy
        {
            get => selectedStrategy;
            set
            {
                if (selectedStrategy != value)
                {
                    selectedStrategy = value;
                    OnPropertyChanged();
                    ComputeItems();
                }
            }
        }

        private List<IPageReplacementStep> clockSolution;
        private List<IPageReplacementStep> optimalSolution;
        private List<IPageReplacementStep> lruSolution;
        private List<IPageReplacementStep> fifoSolution;

        private void ComputeItems()
        {
            Items = new List<string[]>();

            List<IPageReplacementStep> solution = null;
            switch (selectedStrategy)
            {
                case 0: solution = optimalSolution; break;
                case 1: solution = fifoSolution; break;
                case 2: solution = lruSolution; break;
                case 3: solution = clockSolution; break;
            }

            if (solution != null)
            {
                Items = solution.Select(Present).ToList();
            }
            OnPropertyChanged("Items");
        }

        private string[] Present(IPageReplacementStep sim)
        {
            var element = sim.Element;
            var frames = sim.Frames.Select(Present).ToList();
            var frameInformation = sim.FrameInformation.Select(Present).ToList();
            return new string[] {
        $"{element}",
        $"{frames[0]} ({frameInformation[0]})",
        $"{frames[1]} ({frameInformation[1]})",
        $"{frames[2]} ({frameInformation[2]})",
        $"{frames[3]} ({frameInformation[3]})"};
        }

        private string Present(int arg)
        {
            if (arg == int.MaxValue) return "-";//"∞";
            return $"{arg}";
        }

        protected override void Initialize()
        {

            SelectedStrategy = -1;

            var parameters = new PageReplacementParameters()
            {
                MemorySize = 4
            };
            ReferenceRequests = string.Join("", parameters.ReferenceRequests);

            optimalSolution = new OptimalSolver().Solve(parameters).Steps;
            optimalSolution.RemoveAt(0);

            fifoSolution = new FIFOSolver().Solve(parameters).Steps;
            fifoSolution.RemoveAt(0);

            lruSolution = new LRUSolver().Solve(parameters).Steps;
            lruSolution.RemoveAt(0);

            clockSolution = new ClockSolver().Solve(parameters).Steps;
            clockSolution.RemoveAt(0);

        }

    }
}