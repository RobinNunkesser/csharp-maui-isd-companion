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
    internal class Bitencodings_TableGenService : ITableGenService
    {
        private TableGen.TableGen tableGen;
        private IInfoTextService _infoTextService;

        private int _index;
        private int _cellWidth = 25;
        BitencodingParameters _parameters;
        IBitencodingSolution _solution;

        private string[] InfoTexts { get; set; }

        private int currentColumnOfInterest { get; set; }

        public int X_CoordoninatesOfInterest()
        {
            return (currentColumnOfInterest - 3) * _cellWidth;
        }

        public int Y_CoordoninatesOfInterest()
        {
            return 0;
        }

        Color Color_Transparent = Color.Transparent;

        public Bitencodings_TableGenService(BitencodingParameters parameters, IBitencodingSolution solution)
        {
            int length = parameters.Bits.Length;
            tableGen = new TableGen.TableGen(length, 5, 25, 25);
            _index = 0;
            currentColumnOfInterest = 0;
            _parameters = parameters;
            _solution = solution;

            _infoTextService = new BitEncodings_InfoTextService(parameters, solution);
            InfoTexts = _infoTextService.GetInfoTexts();
        }

        public Grid GenerateTable_TableHeader()
        {
            TableGen.TableGen tableGen_TableHeader = new TableGen.TableGen(1, 5, 25, 80);

            for (int i = 0; i < 4; i++)
            {
                tableGen_TableHeader.SetBackGroundColor(i, 0, Color.Transparent);
            }

            List<Label> labels = new List<Label>();

            labels.Add(new Label() { Text =  "Bits" });
            labels.Add(new Label() { Text = "NRZ" });
            labels.Add(new Label() { Text = "NRZ-I" });
            labels.Add(new Label() { Text = "MLT-3" });

            tableGen_TableHeader.AddElement(0, 0, labels[0]);
            tableGen_TableHeader.AddElement(2, 0, labels[1]);
            tableGen_TableHeader.AddElement(3, 0, labels[2]);
            tableGen_TableHeader.AddElement(4, 0, labels[3]);

            return tableGen_TableHeader.Grid;
        }

        public Grid GenerateTable_EmptyTable()
        {
            tableGen.SetBorderForRow(0);
            tableGen.SetBorderForRow(2);
            tableGen.SetBorderForRow(3);
            tableGen.SetBorderForRow(4);

            for (int i = 0; i < _parameters.Bits.Length; i++)
            {
                tableGen.AddCenteredElement(0, i, new Label() { Text = _parameters.Bits[i].ToString() });
            }

            return tableGen.Grid;
        }

        public Grid GenerateTable_NextStep()
        {
            if (_index < _parameters.Bits.Length)
            {
                string[] solutions = { _solution.NRZ[_index], _solution.NRZI[_index], _solution.MLT3[_index] };

                for (int j = 0; j < 3; j++)
                {
                    tableGen.AddCenteredElement(j + 2, _index, new Label() { Text = solutions[j] });
                }
                _index++;
                currentColumnOfInterest = _index;
            }

            return tableGen.Grid;
        }

        public Grid GenerateTable_PreviousStep()
        {
            if (_index > 0)
            {
                _index--;
                for (int j = 0; j < 3; j++)
                {
                    tableGen.RemoveElements(j + 2, _index);
                }
            }

            return tableGen.Grid;
        }

        public Grid GenerateTable_ShowSolution()
        {
            while (_index < _parameters.Bits.Length)
            {
                GenerateTable_NextStep();
            }

            return tableGen.Grid;
        }

        public String GetInfoText()
        {
            if (_index == 0)
            {
                return InfoTexts[_index];
            }
            else
            {
                return InfoTexts[_index - 1];
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