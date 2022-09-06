using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ISDCompanion.Resx;
using ISDCompanion.Services;
using Italbytz.Adapters.Exam.OperatingSystems;
using Italbytz.Infrastructure.Exam.OperatingSystems.PageReplacement;
using Italbytz.Ports.Exam.OperatingSystems;
using TableGen;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ISDCompanion
{
    public class PageReplacementViewModel : Baseclass_Table_ViewModel
    {
        public void AfterRender()
        {
            //Add Picker control to view. This must be done here, defining it in XAML will break the ContentView template because of the height.
            Picker picker = new Picker();
            picker.Title = AppResources.ShowSolution;
            picker.TitleColor = Color.Red;
            picker.Margin = 5;
            picker.SetBinding(Picker.SelectedIndexProperty, new Binding("SelectedStrategy"));

            picker.ItemsSource = new[] {
                "Optimal",
                "FIFO",
                "Least Reacently Used",
                "Second Chance/Clock"
            };

            Exercise_Header = picker;

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

            //loading animation
            //gets automaticly removed when contend finished loading
            Exercise_Content_Header = new ActivityIndicator { IsRunning = true };
            Exercise_Content = new ActivityIndicator { IsRunning = true };

            ComputeItems();

            base.scroll();
        }


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
            AfterRender();

            Info_Button_Clickable = _TableGenService.InfoAvailable();
        }

        private void ComputeItems()
        {
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
                        _TableGenService = new PageReplacement_TableGenService(solution, PageReplacement_TableGenService.Algorithm.Optimal);
                        Exercise_Content_Header = _TableGenService.GenerateTable_TableHeader();
                        Exercise_Content = _TableGenService.GenerateTable_EmptyTable();
                    }
                    break;
                case 1:
                    solution = fifoSolution;
                    if (solution != null)
                    {
                        _TableGenService = new PageReplacement_TableGenService(solution, PageReplacement_TableGenService.Algorithm.FIFO);
                        Exercise_Content_Header = _TableGenService.GenerateTable_TableHeader();
                        Exercise_Content = _TableGenService.GenerateTable_EmptyTable();
                    }
                    break;
                case 2:
                    solution = lruSolution;
                    if (solution != null)
                    {
                        _TableGenService = new PageReplacement_TableGenService(solution, PageReplacement_TableGenService.Algorithm.LRU);
                        Exercise_Content_Header = _TableGenService.GenerateTable_TableHeader();
                        Exercise_Content = _TableGenService.GenerateTable_EmptyTable();
                    }
                    break;
                case 3:
                    solution = clockSolution;
                    if (solution != null)
                    {
                        _TableGenService = new PageReplacement_SecondChanceClock_TableGenService(solution);
                        Exercise_Content_Header = _TableGenService.GenerateTable_TableHeader();
                        Exercise_Content = _TableGenService.GenerateTable_EmptyTable();
                    }
                    break;
            }
            Info_Button_Clickable = _TableGenService.InfoAvailable();
        }
    }
}