using ISDCompanion.Services.Interfaces;
using Italbytz.Ports.Exam.OperatingSystems;
using System;
using System.Collections.Generic;
using System.Text;

namespace ISDCompanion.Services.InfoTextServices
{
    internal class RealtimeScheduling_InfoTextService: IInfoTextService
    {
        List<IPageReplacementStep> _steps;

        public RealtimeScheduling_InfoTextService(List<IPageReplacementStep> steps)
        {
            _steps = steps;
        }

        public string[] GetInfoTexts()
        {
            return null;
        }
    }
}
