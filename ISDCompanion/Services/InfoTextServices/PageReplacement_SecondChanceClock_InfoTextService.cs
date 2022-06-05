using ISDCompanion.Services.Interfaces;
using Italbytz.Ports.Exam.OperatingSystems;
using System;
using System.Collections.Generic;
using System.Text;

namespace ISDCompanion.Services.InfoTextServices
{
    internal class PageReplacement_SecondChanceClock_InfoTextService: IInfoTextService
    {
        List<IPageReplacementStep> _steps;

        public PageReplacement_SecondChanceClock_InfoTextService(List<IPageReplacementStep> steps)
        {
            _steps = steps;
        }

        public string[] GetInfoTexts()
        {
            string[] infoTexts = new string[_steps.Count];

            return infoTexts;
        }
    }
}
