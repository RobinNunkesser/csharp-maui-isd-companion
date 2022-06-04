using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ISDCompanion.Services;
using Italbytz.Adapters.Exam.OperatingSystems;
using Italbytz.Infrastructure.Exam.OperatingSystems.PageReplacement;
using Italbytz.Ports.Exam.OperatingSystems;
using TableGen;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class PageReplacementViewModel : Baseclass_Table_ViewModel
    {

        //public List<string[]> Items { get; set; }
        //private string referenceRequests;
        //public string ReferenceRequests
        //{
        //    get => referenceRequests;
        //    set
        //    {
        //        if (value != referenceRequests)
        //        {
        //            referenceRequests = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        private int selectedStrategy = 0;
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

        protected override void newExercise()
        {
            var parameters = new PageReplacementParameters()
            {
                MemorySize = 4
            };

            optimalSolution = new OptimalSolver().Solve(parameters).Steps;
            optimalSolution.RemoveAt(0);

            fifoSolution = new FIFOSolver().Solve(parameters).Steps;
            fifoSolution.RemoveAt(0);

            lruSolution = new LRUSolver().Solve(parameters).Steps;
            lruSolution.RemoveAt(0);

            clockSolution = new ClockSolver().Solve(parameters).Steps;
            clockSolution.RemoveAt(0);

            ComputeItems();
        }

        private void ComputeItems()
        {
            //Items = new List<string[]>();

            List<IPageReplacementStep> solution = null;

            if (selectedStrategy == -1)
            {
                selectedStrategy = 0;
            }

            switch (selectedStrategy)
            {
                case 0:
                    solution = optimalSolution;
                    if (solution != null)
                    {
                        _TableGenService = new PageReplacement_TableGenService(solution);
                        Table_Header = _TableGenService.GenerateTable_TableHeader();
                        Table = _TableGenService.GenerateTable_EmptyTable();
                    }
                    break;
                case 1:
                    solution = fifoSolution;
                    if (solution != null)
                    {
                        _TableGenService = new PageReplacement_TableGenService(solution);
                        Table_Header = _TableGenService.GenerateTable_TableHeader();
                        Table = _TableGenService.GenerateTable_EmptyTable();
                    }
                    break;
                case 2:
                    solution = lruSolution;
                    if (solution != null)
                    {
                        _TableGenService = new PageReplacement_TableGenService(solution);
                        Table_Header = _TableGenService.GenerateTable_TableHeader();
                        Table = _TableGenService.GenerateTable_EmptyTable();
                    }
                    break;
                case 3:
                    solution = clockSolution;
                    if (solution != null)
                    {
                        _TableGenService = new PageReplacement_SecondChanceClock_TableGenService(solution);
                        Table_Header = _TableGenService.GenerateTable_TableHeader();
                        Table = _TableGenService.GenerateTable_EmptyTable();
                    }
                    break;
            }

            //OnPropertyChanged("Items");
        }
    }
}