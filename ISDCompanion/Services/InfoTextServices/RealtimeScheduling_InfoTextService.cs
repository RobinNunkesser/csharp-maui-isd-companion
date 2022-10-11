using ISDCompanion.Resources.Strings;
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

            string rmsInfo = String.Format(AppResources.InfoText_RealTimeScheduling_RMS_Info);
            string edfInfo = String.Format(AppResources.InfoText_RealTimeScheduling_EDF_Info);


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

                if (step == high.stepID)
                {
                    infoText = String.Format(AppResources.InfoText_RealTimeScheduling_RMS_High, high.process);
                }

                if (step == medium.stepID)
                {
                    infoText = String.Format(AppResources.InfoText_RealTimeScheduling_RMS_Medium, medium.process);
                }

                if (step == low.stepID)
                {
                    infoText = String.Format(AppResources.InfoText_RealTimeScheduling_RMS_Low, low.process);
                }

                infoTexts[index] = infoText + rmsInfo;
                index++;
            }

            foreach (int step in _edf)
            {
                string infoText = "";
                string process = "";

                if (step == 0)
                {
                    process = "A";
                }

                if (step == 1)
                {
                    process = "B";
                }

                if (step == 2)
                {
                    process = "C";
                }

                infoText = String.Format(AppResources.InfoText_RealTimeScheduling_RMS_Low, process);

                infoTexts[index] = infoText + edfInfo;
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
