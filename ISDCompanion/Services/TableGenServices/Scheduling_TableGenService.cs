using ISDCompanion.Resx;
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
    internal class Scheduling_TableGenService : ITableGenService
    {
        public enum Algorithm { ShortestJobFirst, Priority, FirstComeFirstServed }

        private TableGen.TableGen tableGen;

        private int _index;
        private int _cellWidth = 25;

        private SchedulingParameters _parameters;
        private string _solution;
        private string[] _calculation;
        private Algorithm _algorithm;

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

        public Scheduling_TableGenService(SchedulingParameters parameters, string solution, Algorithm algorithm)
        {
            _parameters = parameters;
            _solution = solution;
            _calculation = getCalculation(_parameters, algorithm);
            _algorithm = algorithm;

            tableGen = new TableGen.TableGen(5, 7, 25, 80);
            _index = 0;
            currentRowOfInterest = 0;
        }

        public Grid GenerateTable_TableHeader()
        {
            TableGen.TableGen tableGen_TableHeader = new TableGen.TableGen(1, 7, 25, 100);

            for (int i = 0; i < 7; i++)
            {
                tableGen_TableHeader.SetBackGroundColor(i, 0, Color.Transparent);
            }

            List<Label> labels = new List<Label>();

            labels.Add(new Label() { Text = "Dauer" });
            labels.Add(new Label() { Text = "Priorität" });
            labels.Add(new Label() { Text = "Berechnung" });
            labels.Add(new Label() { Text = "Ergebnis" });

            tableGen_TableHeader.AddElement(0, 0, labels[0]);
            tableGen_TableHeader.AddElement(1, 0, labels[1]);
            tableGen_TableHeader.AddElement(3, 0, labels[2]);
            tableGen_TableHeader.AddElement(6, 0, labels[3]);

            tableGen_TableHeader.SetRowHeight(2, 50);
            tableGen_TableHeader.SetRowHeight(5, 50);

            return tableGen_TableHeader.Grid;
        }

        public Grid GenerateTable_EmptyTable()
        {
            for (int i = 0; i < 5; i++)
            {
                tableGen.AddElement(0, i, new Label() { Text = _parameters.Values[i].ToString() });
                tableGen.AddElement(1, i, new Label() { Text = _parameters.Priorities[i].ToString() });

                for (int j = 0; j < 7; j++)
                {
                    tableGen.SetBackGroundColor(j, i, Color.Transparent);
                }
            }
            tableGen.SetRowHeight(2, 50);
            tableGen.SetRowHeight(5, 50);

            return tableGen.Grid;
        }

        public Grid GenerateTable_NextStep()
        {
            if (_index == 1)
            {
                tableGen.AddElement(6, 2, new Label() { Text = "= " + _solution });

                _index++;
                currentRowOfInterest = _index;
            }

            if (_index == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    tableGen.AddElement(3, i, new Label() { Text = _calculation[i] });
                    tableGen.SetBorderForCell(3, i, TableGen.Border.BorderPosition.Bot);
                }
                tableGen.AddElement(4, 2, new Label() { Text = "5" });

                _index++;
                currentRowOfInterest = _index;
            }
            return tableGen.Grid;
        }

        public Grid GenerateTable_PreviousStep()
        {
            if (_index == 1)
            {
                for (int i = 0; i < 5; i++)
                {
                    tableGen.RemoveElements(3, i);
                    tableGen.RemoveBorder(3, i, TableGen.Border.BorderPosition.Bot);
                }
                tableGen.RemoveElements(4, 2);

                _index--;
                currentRowOfInterest = _index;
            }

            if (_index == 2)
            {
                tableGen.RemoveElements(6, 2);

                _index--;
                currentRowOfInterest = _index;
            }

            return tableGen.Grid;
        }

        public Grid GenerateTable_ShowSolution()
        {
            while (_index < 3)
            {
                GenerateTable_NextStep();
            }

            return tableGen.Grid;
        }

        public String GetInfoText()
        {
            if (_algorithm == Algorithm.ShortestJobFirst)
            {
                return "Die Prozesse werden der Reihenfolge nach abgearbeitet, wobei Prozesse mit geringer Laufzeit priorisiert werden. Für die Gesamtwartezeit wird die Laufzeit eines jeden Prozesses mit der Anzahl an bereiten Prozessen multipliziert, das Ergebnis wird summiert und durch die Anzahl der Prozesse dividiert.";
            }
            else if (_algorithm == Algorithm.Priority)
            {
                return "Die Prozesse werden der Reihenfolge nach entsprechend ihrer Priorität abgearbeitet. Für die Gesamtwartezeit wird die Laufzeit eines jeden Prozesses mit der jeweiligen Priorität multipliziert, das Ergebnis wird summiert und durch die Anzahl der Prozesse dividiert.";
            }
            else if (_algorithm == Algorithm.FirstComeFirstServed)
            {
                return "Die Prozesse werden der Reihenfolge nach abgearbeitet. Für die Gesamtwartezeit wird die Laufzeit eines jeden Prozesses mit der Anzahl an bereiten Prozessen multipliziert, das Ergebnis wird summiert und durch die Anzahl der Prozesse dividiert.";
            }
            else
            {
                return "";
            }
        }

        public bool InfoAvailable()
        {
            return true;
        }

        private string[] getCalculation(SchedulingParameters parameters, Algorithm algorithm)
        {
            string[] output = new string[5];
            int[] priorities = getPriorityAsInteger(parameters);
            int[] values = (int[])parameters.Values.Clone();

            switch (algorithm)
            {
                case Algorithm.ShortestJobFirst:
                    for (int i = 0; i < 5; i++)
                    {
                        Array.Sort(values, priorities);
                        output[i] = values[i].ToString() + " x " + (5 - i);
                        if (i != 4)
                        {
                            output[i] += " +";
                        }
                    }
                    break;
                case Algorithm.Priority:
                    for (int i = 0; i < 5; i++)
                    {
                        output[i] = values[i].ToString() + " x " + priorities[i];
                        if (i != 4)
                        {
                            output[i] += " +";
                        }
                    }
                    break;
                case Algorithm.FirstComeFirstServed:
                    for (int i = 0; i < 5; i++)
                    {
                        output[i] = values[i].ToString() + " x " + (5 - i);
                        if (i != 4)
                        {
                            output[i] += " +";
                        }
                    }

                    break;
            }


            return output;
        }

        private int[] getPriorityAsInteger(SchedulingParameters parameters)
        {
            int[] output = new int[5];

            for (int i = 0; i < 5; i++)
            {
                if (parameters.Priorities[i] == "sehr niedrig")
                {
                    output[i] = 1;
                }
                if (parameters.Priorities[i] == "niedrig")
                {
                    output[i] = 2;
                }
                if (parameters.Priorities[i] == "mittel")
                {
                    output[i] = 3;
                }
                if (parameters.Priorities[i] == "hoch")
                {
                    output[i] = 4;
                }
                if (parameters.Priorities[i] == "sehr hoch")
                {
                    output[i] = 5;
                }
            }

            return output;
        }
    }
}