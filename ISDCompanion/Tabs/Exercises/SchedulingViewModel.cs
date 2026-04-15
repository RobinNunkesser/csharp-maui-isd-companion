using StudyCompanion.Resources.Strings;
using StudyCompanion.Services;
using Italbytz.OperatingSystems;

namespace StudyCompanion
{
    public class SchedulingViewModel : Baseclass_Table_ViewModel
    {
        private SchedulingParameters? parameters;
        private string? sjf;
        private string? prio;
        private string? fcfs;
        private string? rr;

        public void AfterRender()
        {
            //Add Picker control to view. This must be done here, defining it in XAML will break the ContentView template because of the height.
            Picker picker = new Picker();
            picker.Title = AppResources.ShowSolution;
            picker.TitleColor = Colors.Red;
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
            if (parameters == null)
            {
                return;
            }

            ITableGenService? tableGenService = null;

            if (selectedStrategy == -1)
            {
                selectedStrategy = 0;
            }

            switch (selectedStrategy)
            {
                case 0:
                    if (!string.IsNullOrWhiteSpace(sjf))
                    {
                        tableGenService = new Scheduling_TableGenService(parameters, sjf, Scheduling_TableGenService.Algorithm.ShortestJobFirst);
                    }
                    break;
                case 1:
                    if (!string.IsNullOrWhiteSpace(prio))
                    {
                        tableGenService = new Scheduling_TableGenService(parameters, prio, Scheduling_TableGenService.Algorithm.Priority);
                    }
                    break;
                case 2:
                    if (!string.IsNullOrWhiteSpace(fcfs))
                    {
                        tableGenService = new Scheduling_TableGenService(parameters, fcfs, Scheduling_TableGenService.Algorithm.FirstComeFirstServed);
                    }
                    break;
                case 3:
                    if (!string.IsNullOrWhiteSpace(rr))
                    {
                        tableGenService = new Scheduling_RoundRobin_TableGenService(parameters, rr);
                    }
                    break;
            }

            if (tableGenService == null)
            {
                return;
            }

            _TableGenService = tableGenService;
            Exercise_Content_Header = tableGenService.GenerateTable_TableHeader();
            Exercise_Content = tableGenService.GenerateTable_EmptyTable();
            Info_Text = tableGenService.GetInfoText();
            Info_Button_Clickable = tableGenService.InfoAvailable();
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

            Info_Button_Clickable = _TableGenService?.InfoAvailable() ?? false;
        }
    }
}