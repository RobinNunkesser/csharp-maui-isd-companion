using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using OperatingSystemsExam.PageReplacement;
using OperatingSystemsExam.PageReplacement.Adapters;
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
                Items = solution.Select(sim => new string[] {
                            $"{sim.Element}",
                            $"{sim.Frames[0]} ({sim.FrameInformation[0]})",
                            $"{sim.Frames[1]} ({sim.FrameInformation[1]})",
                            $"{sim.Frames[2]} ({sim.FrameInformation[2]})",
                            $"{sim.Frames[3]} ({sim.FrameInformation[3]})"}).ToList();
            }
            OnPropertyChanged("Items");
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

            fifoSolution = new FifoSolver().Solve(parameters);
            fifoSolution.RemoveAt(0);

            lruSolution = new LruSolver().Solve(parameters);
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