using ISDCompanion.Resx;
using ISDCompanion.Services.Interfaces;
using Italbytz.Adapters.Exam.Networks;
using Italbytz.Adapters.Exam.OperatingSystems;
using Italbytz.Ports.Exam.Networks;
using Italbytz.Ports.Exam.OperatingSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISDCompanion.Services.InfoTextServices
{
    internal class CRC_InfoTextService : IInfoTextService
    {
        string[] _calculation;
        string[] _check;

        public CRC_InfoTextService(string[] calculation, string[] check)
        {
            _calculation = calculation;
            _check = check;
        }

        public string[] GetInfoTexts()
        {
            string[] infoTexts = new String[_calculation.Length + _check.Length];
            int index = 0;

            string infoText = "";

            bool calculationFinished = false;
            bool checkFinished = false;

            foreach (string step in _calculation)
            {
                if (index == 0)
                {
                    infoText = String.Format(AppResources.InfoText_CRC_Start, step.Substring(0, 6));
                }
                else if(index == _calculation.Length - 2)
                {
                    infoText = AppResources.InfoText_CRC_check;
                    calculationFinished = true;
                }
                else
                {
                    if(index % 2 == 0)
                    {
                        infoText = AppResources.InfoText_CRC_evenStep;
                    }
                    else
                    {
                        infoText = AppResources.InfoText_CRC_unevenStep;
                    }
                }

                infoTexts[index] = infoText;
                index++;

                if (calculationFinished)
                {
                    break;
                }
            }

            foreach (string step in _check)
            {
                if (index == _calculation.Length - 1)
                {
                    infoText = String.Format(AppResources.InfoText_CRC_check_Start, step.Substring(0, 6), step.Substring(6, 5));
                }
                else if (index == _calculation.Length + _check.Length - 2)
                {
                    infoText = AppResources.InfoText_CRC_check_Finish;
                    checkFinished = true;
                }
                else
                {
                    if(index % 2 == 0)
                    {
                        infoText = AppResources.InfoText_CRC_evenStep;
                    }
                    else
                    {
                        infoText = AppResources.InfoText_CRC_unevenStep;
                    }
                }

                infoTexts[index] = infoText;
                index++;

                if (checkFinished)
                {
                    break;
                }
            }
      

            return infoTexts;
        }
    }
}