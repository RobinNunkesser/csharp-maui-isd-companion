using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ISDCompanion.Interfaces;
using ISDCompanion.Services;
using Italbytz.Adapters.Exam.OperatingSystems;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class RealtimeSchedulingViewModel : Baseclass_Table_ViewModel, IAfterRender
    {
        public void AfterRender()
        {
            var parameters = new RealtimeSchedulingParameters();
            _TableGenService = new RealtimeScheduling_TableGenService(parameters, new EDFSolver().Solve(parameters).Processes, new RMSSolver().Solve(parameters).Processes);

            //loading animation
            //gets automaticly removed when contend finished loading
            Table_Header = new ActivityIndicator { IsRunning = true };
            Table = new ActivityIndicator { IsRunning = true };

            Table_Header = _TableGenService.GenerateTable_TableHeader();
            Table = _TableGenService.GenerateTable_EmptyTable();
            base.scroll();
        }

        protected override void newExercise()
        {
            AfterRender();

            Info_Button_Clickable = _TableGenService.InfoAvailable();
        }
    }
}
