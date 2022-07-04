using ISDCompanion.Services.InfoTextServices;
using ISDCompanion.Services.Interfaces;
using Italbytz.Adapters.Exam.Networks;
using Italbytz.Adapters.Exam.OperatingSystems;
using Italbytz.Ports.Exam.Networks;
using Italbytz.Ports.Exam.OperatingSystems;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ISDCompanion.Services
{
    internal class CRC_TableGenService : ITableGenService
    {
        private TableGen.TableGen tableGen;
        private IInfoTextService _infoTextService;

        private int _index;
        private int _cellWidth = 25;
        CRCParameters _parameters;
        ICRCSolution _solution;

        private char[] request;
        private string[] calculation;
        private string[] calculation_check;

        private string[] InfoTexts { get; set; }

        private int currentRowOfInterest { get; set; }
        private int currentColumnOfInterest { get; set; }

        public int X_CoordoninatesOfInterest()
        {
            return (currentColumnOfInterest - 3) * _cellWidth;
        }

        public int Y_CoordoninatesOfInterest()
        {
            return (currentRowOfInterest - 3) * _cellWidth;
        }

        Color Color_Transparent = Color.Transparent;

        public CRC_TableGenService(CRCParameters parameters, ICRCSolution solution)
        {
            _parameters = parameters;
            _solution = solution;

            calculation = _solution.Calculation.Split(new string[] {"\n\n"}, StringSplitOptions.None);
            calculation_check = _solution.Check.Split(new string[] { "\n\n" }, StringSplitOptions.None);

            request = Convert.ToString(_parameters.Term, 2).ToCharArray();

            _infoTextService = new CRC_InfoTextService(parameters, solution);
            InfoTexts = _infoTextService.GetInfoTexts();

            tableGen = new TableGen.TableGen(calculation[0].Length + calculation_check[0].Length + 3, calculation.Length, 25, 25);
            _index = 0;
            currentRowOfInterest = 0;
        }

        public Grid GenerateTable_TableHeader()
        {
            return null;
        }

        public Grid GenerateTable_EmptyTable()
        {
            char[] request = calculation[0].ToCharArray();
            for (int i = 0; i < calculation[0].Length; i++)
            {
                tableGen.AddCenteredElement(0, i, new Label() { Text = request[i].ToString() });
            }

            return tableGen.Grid;
        }

        private char[] cleanUpCharArray(char[] charArray)
        {
            int i = 0;

            foreach (char c in charArray)
            {
                if (c == '0')
                {
                    charArray[i] = ' ';
                }
                if (c == '1')
                {
                    if (charArray.Length > i + 6)
                    {
                        for (int j = i + 6; j < charArray.Length; j++)
                        {
                            charArray[j] = ' ';
                        }
                    }
                    break;
                }
                i++;
            }

            return charArray;
        }

        public Grid GenerateTable_NextStep()
        {
            if (_index < calculation.Length - 1)
            {
                char[] solutions = calculation[_index + 1].ToCharArray();
                solutions = cleanUpCharArray(solutions);
                

                for (int j = 0; j < calculation[0].Length; j++)
                {
                    tableGen.AddCenteredElement(_index+1, j, new Label() { Text = solutions[j].ToString() });
                }
                _index++;

                currentColumnOfInterest = 0;
                currentRowOfInterest = _index;
            }
            else if ((_index - calculation.Length - 1) < calculation_check.Length - 2)
            {
                char[] solutions = calculation_check[_index - calculation.Length + 1].ToCharArray();
                
                if(_index != calculation.Length - 1)
                {
                    solutions = cleanUpCharArray(solutions);
                }

                for (int j = 0; j < calculation[0].Length; j++)
                {
                    tableGen.AddCenteredElement(_index - calculation.Length + 1, j + calculation[0].Length + 3, new Label() { Text = solutions[j].ToString() });
                }
                _index++;

                currentColumnOfInterest = calculation[0].Length + 3;
                currentRowOfInterest = _index - calculation.Length;
            }

            return tableGen.Grid;
        }

        public Grid GenerateTable_PreviousStep()
        {            
            if (_index >= calculation.Length && _index <= calculation.Length + calculation_check.Length)
            {
                for (int j = 0; j < calculation[0].Length; j++)
                {
                    tableGen.RemoveElements(_index - calculation.Length, j + calculation[0].Length + 3);
                }
                _index--;

                currentColumnOfInterest = calculation[0].Length + 3;
                currentRowOfInterest = _index - calculation.Length;
            }
            else if (_index > 0)
            {
                for (int j = 0; j < calculation[0].Length; j++)
                {
                    tableGen.RemoveElements(_index, j);
                }
                _index--;

                currentColumnOfInterest = 0;
                currentRowOfInterest = _index;
            }
            

            return tableGen.Grid;
        }

        public Grid GenerateTable_ShowSolution()
        {
            while (_index < calculation.Length + calculation_check.Length - 1)
            {
                GenerateTable_NextStep();
            }

            return tableGen.Grid;
        }

        public String GetInfoText()
        {
            if (_index == 0)
            {
                return InfoTexts[0];
            }
            else
            {
                return InfoTexts[0];
            }
        }

        public bool InfoAvailable()
        {
            if (_index == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}