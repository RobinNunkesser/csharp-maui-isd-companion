﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ISDCompanion.Services;
using Italbytz.Adapters.Exam.OperatingSystems;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class BuddyViewModel : ExerciseViewModel
    {
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

        private Buddy_TableGenService _TableGenService { get; set; }

        public ICommand ButtonNextStep { set; get; }
        public ICommand ButtonLastStep { set; get; }
        public ICommand ButtonCompleteSolution { set; get; }
        public ICommand ButtonInfo { set; get; }
        public ICommand ButtonNewExercise { set; get; }


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
            var parameters = new BuddyParameters();
            var solver = new BuddySolver();
            var solution = solver.Solve(parameters);

            _TableGenService = new Buddy_TableGenService(parameters, solution);
            Table_Header = _TableGenService.GenerateTable_TableHeader();
            Table = _TableGenService.GenerateTable_EmptyTable();
        }

        private void nextStep()
        {
            Table = _TableGenService.GenerateTable_NextStep();
        }

        private void lastStep()
        {
            Table = _TableGenService.GenerateTable_PreviousStep();
        }

        private void showCompleteSolution()
        {
            Table = _TableGenService.GenerateTable_ShowSolution();
        }

        private void showInfo()
        {

        }
    }
}
