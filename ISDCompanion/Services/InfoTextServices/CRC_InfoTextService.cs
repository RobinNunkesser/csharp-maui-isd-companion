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
                    infoText = "Die Bitfolge " + step.Substring(0, 6) + " wird um 5 Nullen erweitert. Anschließend wird eine XOR Division mit dem Generatorpolynom 100101 (CRC5) durchgeführt.";
                }
                else if(index == _calculation.Length - 2)
                {
                    infoText = "Die übrig gebliebene Bitfolge wird als Prüfbit bezeichnet und kann im Folgenden für die Prüfung verwendet werden.";
                    calculationFinished = true;
                }
                else
                {
                    if(index % 2 == 0)
                    {
                        infoText = "Auf der resultierenden Bitfolge wird wieder eine XOR Division mit dem Generatorpolynom 100101 durchgeführt.";
                    }
                    else
                    {
                        infoText = "XOR Division wird durchgeführt: \n\n1 über 1 ergibt 0 \n0 über 0 ergibt 0 \n1 über 0 ergibt 1\n0 über 1 ergibt 1 \n\nVoranstehende Nullen können weg gelassen werden. Die resultierende Bitfolge wird wieder auf 6 Zeichen erweitert, indem die vorhandenen Bits (Nullen) der Bitfolge heruntergezogen werden.";
                    }
                }

                infoTexts[index] = infoText;
                index++;

                if (calculationFinished)
                {
                    break;
                }
            }

            foreach(string step in _check)
            {
                if (index == _calculation.Length - 1)
                {
                    infoText = "Die ursprüngliche Bitfolge " + step.Substring(0, 6) + " wird um das Prüfbit " + step.Substring(6, 5) + " erweitert. Anschließend wird eine XOR Division mit dem festen Generatorpolynom 100101 durchgeführt.";
                }
                else if (index == _calculation.Length + _check.Length - 2)
                {
                    infoText = "Ergibt die zum Schluss übrig gebliebene Bitfolge 0, so war die Übertragung erfolgreich.";
                    checkFinished = true;
                }
                else
                {
                    if(index % 2 == 0)
                    {
                        infoText = "Auf der resultierenden Bitfolge wird wieder eine XOR Division mit dem Generatorpolynom 100101 durchgeführt.";
                    }
                    else
                    {
                        infoText = "XOR Division wird durchgeführt: \n\n1 über 1 ergibt 0 \n0 über 0 ergibt 0 \n1 über 0 \n 0 über 1 ergibt 1 \n\nVoranstehende Nullen können weg gelassen werden. Die resultierende Bitfolge wird wieder auf 6 Zeichen erweitert, indem die vorhandenen Bits der Bitfolge heruntergezogen werden.";
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