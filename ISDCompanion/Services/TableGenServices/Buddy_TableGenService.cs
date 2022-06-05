using Italbytz.Adapters.Exam.OperatingSystems;
using Italbytz.Ports.Exam.OperatingSystems;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ISDCompanion.Services
{
    internal class Buddy_TableGenService : ITableGenService
    {
        private TableGen.TableGen tableGen;

        private int _index;
        private int _cellWidth = 25;
        private int _cellHeight = 25;
        BuddyParameters _parameters;
        IBuddySolution _solution;


        private int currentRowOfInterest { get; set; }

        public int X_CoordoninatesOfInterest()
        {
            return 0;
        }

        public int Y_CoordoninatesOfInterest()
        {
            return (currentRowOfInterest - 3) * _cellHeight;
        }

        Color Color_Transparent = Color.Transparent;

        public Buddy_TableGenService(BuddyParameters parameters, IBuddySolution solution)
        {
            tableGen = new TableGen.TableGen(32, 11, 25, 25);
            _index = 0;
            currentRowOfInterest = 0;
            _parameters = parameters;
            _solution = solution;
        }

        public Grid GenerateTable_TableHeader()
        {
            TableGen.TableGen tableGen_TableHeader = new TableGen.TableGen(1, 11, 25, 80);

            for (int i = 0; i < 10; i++)
            {
                tableGen_TableHeader.SetBackGroundColor(i, 0, Color_Transparent);
            }

            List<Label> labels = new List<Label>();

            labels.Add(new Label() { Text = _parameters.Processes[0] + " (" + _parameters.Requests[0] + ")" });
            labels.Add(new Label() { Text = _parameters.Processes[1] + " (" + _parameters.Requests[1] + ")" });
            labels.Add(new Label() { Text = _parameters.Processes[2] + " (" + _parameters.Requests[2] + ")" });
            labels.Add(new Label() { Text = _parameters.Processes[3] + " (" + _parameters.Requests[3] + ")" });
            labels.Add(new Label() { Text = _parameters.Processes[4] + " (" + _parameters.Requests[4] + ")" });
            labels.Add(new Label() { Text = "Free " + _parameters.FreeOrder[0] });
            labels.Add(new Label() { Text = "Free " + _parameters.FreeOrder[1] });
            labels.Add(new Label() { Text = "Free " + _parameters.FreeOrder[2] });
            labels.Add(new Label() { Text = "Free " + _parameters.FreeOrder[3] });
            labels.Add(new Label() { Text = "Free " + _parameters.FreeOrder[4] });

            tableGen_TableHeader.AddElement(1, 0, labels[0]);
            tableGen_TableHeader.AddElement(2, 0, labels[1]);
            tableGen_TableHeader.AddElement(3, 0, labels[2]);
            tableGen_TableHeader.AddElement(4, 0, labels[3]);
            tableGen_TableHeader.AddElement(5, 0, labels[4]);
            tableGen_TableHeader.AddElement(6, 0, labels[5]);
            tableGen_TableHeader.AddElement(7, 0, labels[6]);
            tableGen_TableHeader.AddElement(8, 0, labels[7]);
            tableGen_TableHeader.AddElement(9, 0, labels[8]);
            tableGen_TableHeader.AddElement(10, 0, labels[9]);

            return tableGen_TableHeader.Grid;
        }

        public Grid GenerateTable_EmptyTable()
        {
            tableGen.SetBorderForRow(1);
            tableGen.SetBorderForRow(2);
            tableGen.SetBorderForRow(3);
            tableGen.SetBorderForRow(4);
            tableGen.SetBorderForRow(5);
            tableGen.SetBorderForRow(6);
            tableGen.SetBorderForRow(7);
            tableGen.SetBorderForRow(8);
            tableGen.SetBorderForRow(9);
            tableGen.SetBorderForRow(10);

            for (int i = 0; i < 32; i++)
            {
                tableGen.AddCenteredElement(0, i, new Label() { Text = (i + 1).ToString() });
            }

            return tableGen.Grid;
        }

        public Grid GenerateTable_NextStep()
        {
            if (_index < _solution.History.Count)
            {
                for (int j = 0; j < _solution.History[_index].Length; j++)
                {
                    string letter = _solution.History[_index][j] == -1 ? "" : _parameters.Processes[_solution.History[_index][j]];
                    tableGen.AddCenteredElement(_index + 1, j, new Label() { Text = letter });
                }
                _index++;
                currentRowOfInterest = _index;
            }

            return tableGen.Grid;
        }

        public Grid GenerateTable_PreviousStep()
        {
            //To Do

            return tableGen.Grid;
        }

        public Grid GenerateTable_ShowSolution()
        {
            while (_index < _solution.History.Count - 1)
            {
                GenerateTable_NextStep();
            }

            return tableGen.Grid;
        }
    }
}