using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ISDCompanion.Services;
using Italbytz.Adapters.Exam.OperatingSystems;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class RealtimeSchedulingViewModel : ExerciseViewModel
    {
        private string requests;
        public string Requests
        {
            get => requests;
            set
            {
                if (value != requests)
                {
                    requests = value;
                    OnPropertyChanged();
                }
            }
        }

        private string edfSolution;
        public string EDFSolution
        {
            get => edfSolution;
            set
            {
                if (value != edfSolution)
                {
                    edfSolution = value;
                    OnPropertyChanged();
                }
            }
        }

        private string rmsSolution;
        public string RMSSolution
        {
            get => rmsSolution;
            set
            {
                if (value != rmsSolution)
                {
                    rmsSolution = value;
                    OnPropertyChanged();
                }
            }
        }

        public Grid Table
        {
            get
            {
                return _Table;
            }
            private set
            {
                _Table = value;
                OnPropertyChanged();
            }
        }

        private Grid _Table { get; set; }

        public Grid Table_Header
        {
            get
            {
                return _Table_Header;
            }
            private set
            {
                _Table_Header = value;
                OnPropertyChanged();
            }
        }

        private Grid _Table_Header { get; set; }




        private RealtimeScheduling_TableGenService _TableGenService { get; set; }

        public ICommand ButtonNextStep { set; get; }
        public ICommand ButtonLastStep { set; get; }
        public ICommand ButtonCompleteSolution { set; get; }
        public ICommand ButtonInfo { set; get; }
        public ICommand ButtonNewExercise { set; get; }

        private bool lastActionWasNextStep { get; set; } = false;
        private bool lastActionWasLastStep { get; set; } = false;

        protected override void Initialize()
        {
            ButtonNewExercise = new Command(newExercise, () => true);
            ButtonNextStep = new Command(nextStep, () => true);
            ButtonLastStep = new Command(lastStep, () => true);
            ButtonCompleteSolution = new Command(showCompleteSolution, () => true);
            ButtonInfo = new Command(showInfo, () => true);

            newExercise();
        }

        private void newExercise()
        {
            var parameters = new RealtimeSchedulingParameters();

            _TableGenService = new RealtimeScheduling_TableGenService();
            Table_Header = _TableGenService.GenerateTable_RealtimeScheduling_TableHeader(parameters);
            Table = _TableGenService.GenerateTable_RealtimeScheduling_EmptyTable(parameters, new EDFSolver().Solve(parameters).Processes, new RMSSolver().Solve(parameters).Processes);
        }

        private void nextStep()
        {
            lastActionWasNextStep = true;
            if (lastActionWasLastStep )
            {
                lastActionWasLastStep = false;
                if(_TableGenService.currentColumnOfInterest != 0)
                {
                    this.nextStep();
                }
            }
            Table = _TableGenService.NextStep_RealtimeScheduling();
        }

        private void lastStep()
        {
            lastActionWasLastStep = true;
            if (lastActionWasNextStep)
            {
                lastActionWasNextStep = false;
                if (_TableGenService.currentColumnOfInterest != 31)
                {
                    this.lastStep();
                }
            }
            Table = _TableGenService.LastStep_RealtimeScheduling();
        }

        private void showCompleteSolution()
        {
            Table = _TableGenService.ShowSolution_RealtimeScheduling();
        }

        private void showInfo()
        {

        }
    }
}
