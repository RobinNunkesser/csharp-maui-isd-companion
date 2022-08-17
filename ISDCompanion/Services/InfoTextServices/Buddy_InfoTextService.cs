using ISDCompanion.Resx;
using ISDCompanion.Services.Interfaces;
using Italbytz.Adapters.Exam.OperatingSystems;
using Italbytz.Ports.Exam.OperatingSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISDCompanion.Services.InfoTextServices
{
    internal class Buddy_InfoTextService: IInfoTextService
    {
        BuddyParameters _parameters;
        IBuddySolution _solution;

        public Buddy_InfoTextService(BuddyParameters parameters, IBuddySolution solution)
        {
            _parameters = parameters;
            _solution = solution;
        }

        public string[] GetInfoTexts()
        {
            string[] infoTexts = new string[_solution.History.Count];
            int index = 0;

            int process_Size_Allocated = 0;

            foreach (int[] step in _solution.History)
            {
                string infoText = "";

                //Anweisungen abarbeiten
                //Prozessanforderungen
                if(index < _parameters.Processes.Length)
                {
                    process_Size_Allocated = (from num in _solution.History[index]
                                              where num == index
                                              select num).Count();
                    process_Size_Allocated *= 32;

                    infoText = String.Format(AppResources.InfoText_Buddy_Request, _parameters.Processes[index], _parameters.Requests[index], process_Size_Allocated);
                }
                else
                {
                    infoText = String.Format(AppResources.InfoText_Buddy_Final, _parameters.FreeOrder[index - 5]);
                 }

                infoTexts[index] = infoText;
                index++;
            }

            return infoTexts;
        }
    }
}