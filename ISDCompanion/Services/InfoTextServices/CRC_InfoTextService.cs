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
        CRCParameters _parameters;
        ICRCSolution _solution;

        public CRC_InfoTextService(CRCParameters parameters, ICRCSolution solution)
        {
            _parameters = parameters;
            _solution = solution;
        }

        public string[] GetInfoTexts()
        {
            string[] infoTexts = new String[]{ "" };
      

            return infoTexts;
        }
    }
}