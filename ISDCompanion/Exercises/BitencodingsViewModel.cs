﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ISDCompanion.Interfaces;
using ISDCompanion.Services;
using Italbytz.Adapters.Exam.Networks;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class BitencodingsViewModel : Baseclass_Table_ViewModel, IAfterRender
    {
        public void AfterRender()
        {
            var parameters = new BitencodingParameters();
            var solver = new BitencodingSolver();
            var solution = solver.Solve(parameters);

            _TableGenService = new Bitencodings_TableGenService(parameters, solution);
            //loading animation
            //gets automaticly removed when contend finished loading
            Exercise_Content_Header = new ActivityIndicator { IsRunning = true };
            Exercise_Content = new ActivityIndicator { IsRunning = true };

            Exercise_Content_Header = _TableGenService.GenerateTable_TableHeader();
            Exercise_Content = _TableGenService.GenerateTable_EmptyTable();

            Info_Text = _TableGenService.GetInfoText();
            Info_Button_Clickable = _TableGenService.InfoAvailable();
            base.scroll();
        }

        protected override void newExercise()
        {
            AfterRender();

            Info_Button_Clickable = _TableGenService.InfoAvailable();
        }
    }
}