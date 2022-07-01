using ISDCompanion.Services.Interfaces;
using Italbytz.Ports.Exam.OperatingSystems;
using System;
using System.Collections.Generic;
using System.Text;

namespace ISDCompanion.Services.InfoTextServices
{
    internal class RealtimeScheduling_InfoTextService : IInfoTextService
    {
        IRealtimeSchedulingParameters _parameters;
        int[] _rms;
        int[] _edf;

        string[] processes;

        public RealtimeScheduling_InfoTextService(IRealtimeSchedulingParameters parameters, int[] edf, int[] rms)
        {
            _parameters = parameters;
            _rms = rms;
            _edf = edf;

            processes = new string[3];
        }

        public string[] GetInfoTexts()
        {
            string[] infoTexts = new string[_rms.Length + _edf.Length];

            processRequest high = new processRequest();
            processRequest medium = new processRequest();
            processRequest low = new processRequest();


            if (_parameters.Requests[0].Item2 <= _parameters.Requests[1].Item2 && _parameters.Requests[0].Item2 <= _parameters.Requests[2].Item2)
            {
                high.process = "A"; 
                high.stepID = 0;
            }
            if (_parameters.Requests[1].Item2 <= _parameters.Requests[0].Item2 && _parameters.Requests[1].Item2 <= _parameters.Requests[2].Item2)
            {
                high.process = "B"; 
                high.stepID = 1;
            }
            if (_parameters.Requests[2].Item2 <= _parameters.Requests[0].Item2 && _parameters.Requests[2].Item2 <= _parameters.Requests[1].Item2)
            {
                high.process = "C"; 
                high.stepID = 2;
            }

            if (_parameters.Requests[0].Item2 >= _parameters.Requests[1].Item2 && _parameters.Requests[0].Item2 >= _parameters.Requests[2].Item2)
            {
                low.process = "A"; 
                low.stepID = 0;
            }
            if (_parameters.Requests[1].Item2 >= _parameters.Requests[0].Item2 && _parameters.Requests[1].Item2 >= _parameters.Requests[2].Item2)
            {
                low.process = "B"; 
                low.stepID = 1;
            }
            if (_parameters.Requests[2].Item2 >= _parameters.Requests[0].Item2 && _parameters.Requests[2].Item2 >= _parameters.Requests[1].Item2)
            {
                low.process = "C";
                low.stepID = 2;
            }

            if ((high.process == "A" && low.process == "B") || (high.process == "B" && low.process == "A"))
            {
                medium.process = "C"; 
                medium.stepID = 2;
            }

            if ((high.process == "A" && low.process == "C") || (high.process == "C" && low.process == "A"))
            {
                medium.process = "B"; 
                medium.stepID = 1;
            }

            if ((high.process == "B" && low.process == "C") || (high.process == "C" && low.process == "B"))
            {
                medium.process = "A"; 
                medium.stepID = 0;
            }

            int index = 0;

            foreach (int step in _rms)
            {
                string infoText = "";

                if(step == high.stepID)
                {
                    infoText = "Da der Prozess " + high.process + " gemäß RMS die höchste Priorität hat, wird er sofort ausgeführt. \n\nDer Prozess kann Prozesse mit niedriger und mit mittlerer Priorität unterbrechen.";
                }

                if (step == medium.stepID)
                {
                    infoText = "Der Prozess " + medium.process + " hat gemäß RMS mittlere Priorität. \n\nEr wird gestartet, da kein Prozess mit höherer Priorität ansteht. \n\nDer Prozess kann einen Prozess mit niedriger Priorität unterbrechen und von einem Prozess mit hoher Priorität unterbrochen werden.";
                }

                if (step == low.stepID)
                {
                    infoText = "Der Prozess " + low.process + " hat gemäß RMS die niedrigste Priorität. \n\nEr wird gestartet, da kein Prozess mit höherer Priorität ansteht. \n\nDer Prozess kann von einem Prozess mit mittlerer oder hoher Priorität unterbrochen werden.";
                }

                infoTexts[index] = infoText;
                index++;
            }

            foreach (int step in _edf)
            {
                string infoText = "";

                if (step == high.stepID)
                {
                    infoText = "Da der Prozess " + high.process + " gemäß RMS die höchste Priorität hat, wird er sofort ausgeführt. \n\nDer Prozess kann Prozesse mit niedriger und mit mittlerer Priorität unterbrechen.";
                }

                if (step == medium.stepID)
                {
                    infoText = "Der Prozess " + medium.process + " hat gemäß RMS mittlere Priorität. \n\nEr wird gestartet, da kein Prozess mit höherer Priorität ansteht. \n\nDer Prozess kann einem Prozess mit niedriger Priorität unterbrechen und von einem Prozess mit hoher Priorität unterbrochen werden.";
                }

                if (step == low.stepID)
                {
                    infoText = "Der Prozess " + low.process + " hat gemäß RMS die niedrigste Priorität. \n\nEr wird gestartet, da kein Prozess mit höherer Priorität ansteht. \n\nDer Prozess kann von einem Prozess mit mittlerer oder hoher Priorität unterbrochen werden.";
                }

                infoTexts[index] = infoText;
                index++;
            }



            return infoTexts;
        }

        struct processRequest
        {
            public string process;
            public int stepID;
        }
    }
}
