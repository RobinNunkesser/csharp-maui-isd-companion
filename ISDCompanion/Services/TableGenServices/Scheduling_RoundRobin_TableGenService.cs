using ISDCompanion.Resx;
using ISDCompanion.Services.InfoTextServices;
using ISDCompanion.Services.Interfaces;
using Italbytz.Adapters.Exam.Networks;
using Italbytz.Adapters.Exam.OperatingSystems;
using Italbytz.Ports.Exam.Networks;
using Italbytz.Ports.Exam.OperatingSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ISDCompanion.Services
{
    internal class Scheduling_RoundRobin_TableGenService : ITableGenService
    {
        public enum Algorithm { ShortestJobFirst, Priority, FirstComeFirstServed }

        private TableGen.TableGen tableGen;
        private IInfoTextService _infoTextService;

        private int _index;
        private int _cellWidth = 25;

        private SchedulingParameters _parameters;
        private string _solution;

        private string[] stepDescription;
        private Dictionary<int, string[]> stepValues;

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

        public Scheduling_RoundRobin_TableGenService(SchedulingParameters parameters, string solution)
        {
            _parameters = parameters;
            _solution = solution;
            //_calculation = 

            stepDescription = new string[10];
            stepValues = new Dictionary<int, string[]>();

            getCalculation(_parameters);


            //_infoTextService = new CRC_InfoTextService(calculation, calculation_check);
            //InfoTexts = _infoTextService.GetInfoTexts();
            //_tableColumnCount = calculation[0].Length + calculation_check[0].Length + 3;

            tableGen = new TableGen.TableGen(5, 18, 25, 80);
            _index = 0;
            currentRowOfInterest = 0;
        }

        public Grid GenerateTable_TableHeader()
        {
            TableGen.TableGen tableGen_TableHeader = new TableGen.TableGen(1, 18, 25, 100);

            for (int i = 0; i < 18; i++)
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
            tableGen_TableHeader.AddElement(17, 0, labels[3]);

            return tableGen_TableHeader.Grid;
        }

        public Grid GenerateTable_EmptyTable()
        {
            for (int i = 0; i < 5; i++)
            {
                tableGen.AddElement(0, i, new Label() { Text = _parameters.Values[i].ToString() });
                tableGen.AddElement(1, i, new Label() { Text = _parameters.Priorities[i].ToString() });

                for (int j = 0; j < 18; j++)
                {
                    tableGen.SetBackGroundColor(j, i, Color.Transparent);
                }
            }

            return tableGen.Grid;
        }

        public Grid GenerateTable_NextStep()
        {
            if (_index == 6)
            {
                _index++;
                tableGen.AddElement(2 * _index + 3, 2, new Label() { Text = "= " + _solution });

                currentRowOfInterest = _index;
            }
            if (_index == 5)
            {
                _index++;
                string[] values = new string[5];
                stepValues.TryGetValue(_index, out values);
                tableGen.AddElement(2 * _index + 2, 0, new Label() { Text = stepDescription[_index] }, 5, 1);
                for (int i = 0; i < 5; i++)
                {
                    tableGen.AddElement(2 * _index + 3, i, new Label() { Text = values[i] });
                    tableGen.SetBorderForCell(2 * _index + 3, i, TableGen.Border.BorderPosition.Bot);
                }
                tableGen.AddElement(2 * _index + 4, 2, new Label() { Text = "5" });
                currentRowOfInterest = _index;
            }
            if (_index >= 0 && _index < 5)
            {
                _index++;
                string[] values = new string[5];
                stepValues.TryGetValue(_index, out values);
                tableGen.AddElement(2 * _index + 1, 0, new Label() { Text = stepDescription[_index] }, 5, 1);
                for (int i = 0; i < 5; i++)
                {
                    tableGen.AddElement(2 * _index + 2, i, new Label() { Text = values[i] });
                }
                currentRowOfInterest = _index;
            }


            return tableGen.Grid;
        }

        public Grid GenerateTable_PreviousStep()
        {
            if (_index > 0 && _index < 6)
            {
                string[] values = new string[5];
                tableGen.RemoveElements(2 * _index + 1, 0);
                for (int i = 0; i < 5; i++)
                {
                    tableGen.RemoveElements(2 * _index + 2, i);
                }
                _index--;
                currentRowOfInterest = _index;
            }
            if (_index == 6)
            {
                tableGen.RemoveElements(2 * _index + 2, 0);
                for (int i = 0; i < 5; i++)
                {
                    tableGen.RemoveElements(2 * _index + 3, i);
                    tableGen.RemoveBorder(2 * _index + 3, i, TableGen.Border.BorderPosition.Bot);
                }
                tableGen.RemoveElements(2 * _index + 4, 2);
                _index--;
                currentRowOfInterest = _index;
            }
            if (_index == 7)
            {
                tableGen.RemoveElements(2 * _index + 3, 2);

                _index--;
                currentRowOfInterest = _index;
            }

            return tableGen.Grid;
        }

        public Grid GenerateTable_ShowSolution()
        {
            while (_index < 7)
            {
                GenerateTable_NextStep();
            }

            return tableGen.Grid;
        }

        public String GetInfoText()
        {
            return "Nach dem Round Robin Verfahren wird jedem bereiten Prozess die selbe Rechenzeit gewährt. Wird ein Prozess vollendet, so wird die Rechenzeit erneut aufgeteilt. Die Gesamtwartezeit ergibt sich aus den Zeiteinheiten, welche es gebraucht, bis ein jeder Prozess fertig war, multipliziert mit der jeweiligen Anzahl an bereiten Prozessen, dividiert durch die Gesamtanzahl der Prozesse.";
        }

        public bool InfoAvailable()
        {
            return true;
        }

        private void getCalculation(SchedulingParameters parameters)
        {
            int[] values = (int[])parameters.Values.Clone();
            int[] times = new int[5];

            stepDescription[0] = "Beginn:";
            stepValues[0] = values.Select(x => x.ToString()).ToArray();

            for (int i = 0; i < 5; i++)
            {
                int lowestvalue = int.MaxValue;
                int amountNotNull = 0;
                for (int j = 0; j < values.Length; j++)
                {
                    if (values[j] != 0)
                    {
                        amountNotNull++;
                        if (values[j] < lowestvalue)
                        {
                            lowestvalue = values[j];
                        }
                    }
                }
                int time = lowestvalue * amountNotNull;
                times[i] = time;

                for (int j = 0; j < values.Length; j++)
                {
                    if (values[j] != 0)
                    {
                        values[j] = values[j] - lowestvalue;
                    }
                }
                if (time == 1)
                {
                    stepDescription[i + 1] = "Nach " + time + " Zeiteinheit:";
                }
                else
                {
                    stepDescription[i + 1] = "Nach " + time + " Zeiteinheiten:";
                }
                stepValues[i + 1] = values.Select(x => x.ToString()).ToArray();
            }

            string[] timesStringArray = new string[5];
            for (int i = 0; i < times.Length; i++)
            {
                timesStringArray[i] = times[i].ToString() + " x " + (5 - i);
                if (i != 4)
                {
                    timesStringArray[i] += " +";
                }
            }

            stepDescription[6] = "Wartezeit:";
            stepValues[6] = timesStringArray;
        }
    }
}