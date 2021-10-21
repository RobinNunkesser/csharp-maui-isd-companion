using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Italbytz.Adapters.Exam.OperatingSystems;
using Italbytz.Infrastructure.Exam.OperatingSystems.PageReplacement;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class PageReplacementViewModel : INotifyPropertyChanged
    {

        public List<string[]> Items { get; set; }
        public ICommand NewParams { get; set; }
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

        private List<SimulationResult> clockSolution;
        private List<SimulationResult> optimalSolution;
        private List<SimulationResult> lruSolution;
        private List<SimulationResult> fifoSolution;

        public PageReplacementViewModel()
        {
            Initialize();
            NewParams = new Command(Initialize);
        }

        private void ComputeItems()
        {
            Items = new List<string[]>();

            List<SimulationResult> solution = null;
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

        private string[] Present(SimulationResult sim)
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

        private void Initialize()
        {

            SelectedStrategy = -1;

            var parameters = new PageReplacementParameters()
            {
                MemorySize = 4
            };
            ReferenceRequests = string.Join("", parameters.ReferenceRequests);

            optimalSolution = new OptimalSolver().Solve(parameters);
            optimalSolution.RemoveAt(0);

            fifoSolution = new FIFOSolver().Solve(parameters);
            fifoSolution.RemoveAt(0);

            lruSolution = new LRUSolver().Solve(parameters);
            lruSolution.RemoveAt(0);

            clockSolution = new ClockSolver().Solve(parameters);
            clockSolution.RemoveAt(0);

        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion
    }
}