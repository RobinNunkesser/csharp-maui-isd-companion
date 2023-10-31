using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using StudyCompanion.Services;
using Italbytz.Adapters.Exam.Networks;

namespace StudyCompanion
{
    public class CRCViewModel : Baseclass_Table_ViewModel
    {
        public void AfterRender()
        {
            var parameters = new CRCParameters();
            var solver = new CRCSolver();
            var solution = solver.Solve(parameters);

            _TableGenService = new CRC_TableGenService(parameters, solution);
            //loading animation
            //gets automaticly removed when contend finished loading
            //Exercise_Header = new ActivityIndicator { IsRunning = true };
            Exercise_Content = new ActivityIndicator { IsRunning = true };

            //Exercise_Header = _TableGenService.GenerateTable_TableHeader();
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
