using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ISDCompanion.Services;
using Italbytz.Adapters.Exam.OperatingSystems;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class BuddyViewModel : Baseclass_Table_ViewModel
    {
        public void AfterRender()
        {
            var parameters = new BuddyParameters();
            var solver = new BuddySolver();
            var solution = solver.Solve(parameters);

            _TableGenService = new Buddy_TableGenService(parameters, solution);
            //loading animation
            //gets automaticly removed when contend finished loading
            Exercise_Content_Header = new ActivityIndicator { IsRunning = true };
            Exercise_Content = new ActivityIndicator { IsRunning = true };

            Exercise_Content_Header = _TableGenService.GenerateTable_TableHeader();
            Exercise_Content = _TableGenService.GenerateTable_EmptyTable();

            base.scroll();


        }

        protected override void newExercise()
        {
            AfterRender();

            Info_Button_Clickable = _TableGenService.InfoAvailable();
        }
    }
}
