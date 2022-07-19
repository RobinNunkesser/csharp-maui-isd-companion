using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ISDCompanion.Interfaces;
using ISDCompanion.Resx;
using ISDCompanion.Services;
using Italbytz.Adapters.Exam.OperatingSystems;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class SchedulingViewModel : Baseclass_Table_ViewModel, IAfterRender
    {
        SchedulingParameters parameters;

        public void AfterRender()
        {
            //Add Picker control to view. This must be done here, defining it in XAML will break the ContentView template because of the height.
            Picker picker = new Picker();
            picker.Title = AppResources.ShowSolution;
            picker.TitleColor = Color.Red;
            picker.Margin = 5;
            picker.SetBinding(Picker.SelectedIndexProperty, new Binding("SelectedStrategy"));

            picker.ItemsSource = new[] {
                "Shortest Job First",
                "Priority",
                "First-come first-served",
                "Round Robin"
            };

            Exercise_Header = picker;

            //loading animation
            //gets automaticly removed when contend finished loading
            Exercise_Content_Header = new ActivityIndicator { IsRunning = true };
            Exercise_Content = new ActivityIndicator { IsRunning = true };


            parameters = new SchedulingParameters();

            sjf = $"{new ShortestJobFirstSolver().Solve(parameters).Time}";
            prio = $"{new PrioritySchedulingSolver().Solve(parameters).Time}";
            fcfs = $"{new FCFSSolver().Solve(parameters).Time}";
            rr = $"{new RoundRobinSolver().Solve(parameters).Time}";

            ComputeItems();
            base.scroll();
        }

        private void ComputeItems()
        {
            string solution = null;

            if (selectedStrategy == -1)
            {
                selectedStrategy = 0;
            }

            switch (selectedStrategy)
            {
                case 0:
                    solution = sjf;
                    if (solution != null)
                    {
                        _TableGenService = new Scheduling_TableGenService(parameters, solution, Scheduling_TableGenService.Algorithm.ShortestJobFirst);
                        Exercise_Content_Header = _TableGenService.GenerateTable_TableHeader();
                        Exercise_Content = _TableGenService.GenerateTable_EmptyTable();
                    }
                    break;
                case 1:
                    solution = prio;
                    if (solution != null)
                    {
                        _TableGenService = new Scheduling_TableGenService(parameters, solution, Scheduling_TableGenService.Algorithm.Priority);
                        Exercise_Content_Header = _TableGenService.GenerateTable_TableHeader();
                        Exercise_Content = _TableGenService.GenerateTable_EmptyTable();
                    }
                    break;
                case 2:
                    solution = fcfs;
                    if (solution != null)
                    {
                        _TableGenService = new Scheduling_TableGenService(parameters, solution, Scheduling_TableGenService.Algorithm.FirstComeFirstServed);
                        Exercise_Content_Header = _TableGenService.GenerateTable_TableHeader();
                        Exercise_Content = _TableGenService.GenerateTable_EmptyTable();
                    }
                    break;
                case 3:
                    solution = rr;
                    if (solution != null)
                    {
                        _TableGenService = new Scheduling_RoundRobin_TableGenService(parameters, solution);
                        Exercise_Content_Header = _TableGenService.GenerateTable_TableHeader();
                        Exercise_Content = _TableGenService.GenerateTable_EmptyTable();
                    }
                    break;
            }

            Info_Text = _TableGenService.GetInfoText();
            Info_Button_Clickable = _TableGenService.InfoAvailable();
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

        protected override void newExercise()
        {
            AfterRender();

            Info_Button_Clickable = _TableGenService.InfoAvailable();
        }

        private string sjf;

        private string prio;

        private string fcfs;

        private string rr;
    }
}