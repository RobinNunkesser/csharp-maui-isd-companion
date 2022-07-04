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
    internal class BitEncodings_InfoTextService : IInfoTextService
    {
        BitencodingParameters _parameters;
        IBitencodingSolution _solution;

        public BitEncodings_InfoTextService(BitencodingParameters parameters, IBitencodingSolution solution)
        {
            _parameters = parameters;
            _solution = solution;
        }

        public string[] GetInfoTexts()
        {
            string[] infoTexts = new string[_parameters.Bits.Length];
            

            return infoTexts;
        }
    }
}