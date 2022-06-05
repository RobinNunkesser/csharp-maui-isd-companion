using Italbytz.Ports.Exam.OperatingSystems;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ISDCompanion.Services
{
    internal class RealtimeScheduling_TableGenService : ITableGenService
    {
        private TableGen.TableGen tableGen;

        private int[] _edf;
        private int _edfIndex;
        private int[] _rms;
        private int _rmsIndex;
        private int _cellWidth = 25;
        IRealtimeSchedulingParameters _parameters;

        private bool _lastActionWasNextStep = false;
        private bool _lastActionWasPreviousStep = false;


        private int currentColumnOfInterest { get;  set; }

        public int X_CoordoninatesOfInterest()
        {
            return (currentColumnOfInterest - 5) * _cellWidth;
        }

        public int Y_CoordoninatesOfInterest()
        {
            return 0;
        }

        Color Color_A = Color.FromRgb(200, 0, 0);
        Color Color_B = Color.FromRgb(0, 0, 200);
        Color Color_C = Color.FromRgb(0, 200, 0);


        Color Color_Transparent = Color.Transparent;

        public RealtimeScheduling_TableGenService(IRealtimeSchedulingParameters parameters, int[] edf, int[] rms)
        {
            tableGen = new TableGen.TableGen(32, 11, 25, 25);
            _edfIndex = 0;
            _rmsIndex = 0;
            currentColumnOfInterest = 0;
            _parameters = parameters;
            _edf = edf;
            _rms = rms;
        }


        public Grid GenerateTable_TableHeader()
        {
            TableGen.TableGen tableGen_TableHeader = new TableGen.TableGen(1, 11, 25, 80);

            tableGen_TableHeader.SetRowHeight(3, 10);
            tableGen_TableHeader.SetRowHeight(7, 10);

            for(int i = 0; i <= 10; i++)
            {
                tableGen_TableHeader.SetBackGroundColor(i, 0, Color_Transparent);
            }

            tableGen_TableHeader.SetColumnWidth(0, 80);

            tableGen_TableHeader.SetRowHeight(3, 10);
            tableGen_TableHeader.SetRowHeight(7, 10);

            List<Label> labels = new List<Label>();

            labels.Add(new Label() { Text = "A" });
            labels.Add(new Label() { Text = "B" });
            labels.Add(new Label() { Text = "C" });
            labels.Add(new Label() { Text = "RMS A" });
            labels.Add(new Label() { Text = "RMS B" });
            labels.Add(new Label() { Text = "RMS C" });
            labels.Add(new Label() { Text = "EDF A" });
            labels.Add(new Label() { Text = "EDF B" });
            labels.Add(new Label() { Text = "EDF C" });

            tableGen_TableHeader.AddElement(0, 0, labels[0]);
            tableGen_TableHeader.AddElement(1, 0, labels[1]);
            tableGen_TableHeader.AddElement(2, 0, labels[2]);

            tableGen_TableHeader.AddElement(4, 0, labels[3]);
            tableGen_TableHeader.AddElement(5, 0, labels[4]);
            tableGen_TableHeader.AddElement(6, 0, labels[5]);

            tableGen_TableHeader.AddElement(8, 0, labels[6]);
            tableGen_TableHeader.AddElement(9, 0, labels[7]);
            tableGen_TableHeader.AddElement(10, 0, labels[8]);

            return tableGen_TableHeader.Grid;
        }

        public Grid GenerateTable_EmptyTable()
        {
            tableGen.SetBorderForRow(0);
            tableGen.SetBorderForRow(1);
            tableGen.SetBorderForRow(2);

            tableGen.SetBorderForRow(4);
            tableGen.SetBorderForRow(5);
            tableGen.SetBorderForRow(6);

            tableGen.SetBorderForRow(8);
            tableGen.SetBorderForRow(9);
            tableGen.SetBorderForRow(10);


            tableGen.SetRowHeight(3, 10);
            tableGen.SetRowHeight(7, 10);


            int process = 0;
            foreach (System.ValueTuple<int, int> request in _parameters.Requests)
            {
                int index = 0;
                while (index < 32)
                {
                    for (int i = 0; i < request.Item1; i++)
                    {
                        int j = index + i;
                        if (j <= 31)
                        {
                            if (process == 0)
                            {
                                tableGen.SetBackGroundColor(0, j, Color_A);
                            }
                            if (process == 1)
                            {
                                tableGen.SetBackGroundColor(1, j, Color_B);
                            }
                            if (process == 2)
                            {
                                tableGen.SetBackGroundColor(2, j, Color_C);
                            }
                        }

                    }
                    index = index + request.Item2;
                }
                process++;
            }

            return tableGen.Grid;
        }

        public Grid GenerateTable_NextStep()
        {
           _lastActionWasNextStep = true;
            if (_lastActionWasPreviousStep)
            {
                _lastActionWasPreviousStep = false;
                if(_rmsIndex != 0 && _rmsIndex < _rms.Length -1)
                {
                    _rmsIndex++;
                }
                if(_edfIndex != 0 && _edfIndex < _edf.Length -1 && _rmsIndex == _rms.Length - 1)
                {
                    _edfIndex++;
                }
            }

            if (_rmsIndex < _rms.Length - 1)
            {
                int currentValue = _rms[_rmsIndex];
                int i = _rmsIndex;
                while (_rms[i] == currentValue)
                {
                    if (_rms[i] == 0)
                    {
                        tableGen.SetBackGroundColor(4, i, Color_A);
                    }
                    if (_rms[i] == 1)
                    {
                        tableGen.SetBackGroundColor(5, i, Color_B);
                    }
                    if (_rms[i] == 2)
                    {
                        tableGen.SetBackGroundColor(6, i, Color_C);
                    }
                    i++;
                    if (i > _rms.Length - 1)
                    {
                        i = 31;
                        break;
                    }
                }
                _rmsIndex = i;
                currentColumnOfInterest = _rmsIndex;
            }
            else
            {
                if (_edfIndex < _edf.Length - 1)
                {
                    int currentValue = _edf[_edfIndex];
                    int i = _edfIndex;
                    while (_edf[i] == currentValue)
                    {
                        if (_edf[i] == 0)
                        {
                            tableGen.SetBackGroundColor(8, i, Color_A);
                        }
                        if (_edf[i] == 1)
                        {
                            tableGen.SetBackGroundColor(9, i, Color_B);
                        }
                        if (_edf[i] == 2)
                        {
                            tableGen.SetBackGroundColor(10, i, Color_C);
                        }
                        i++;
                        if (i > _edf.Length - 1)
                        {
                            i = 31;
                            break;
                        }
                    }
                    _edfIndex = i;
                    currentColumnOfInterest = _edfIndex;
                }
            }

            return tableGen.Grid;
        }

        public Grid GenerateTable_PreviousStep()
        {
            _lastActionWasPreviousStep = true;
            if (_lastActionWasNextStep)
            {
                _lastActionWasNextStep = false;
                if(_rmsIndex != _rms.Length -1 && _rmsIndex != 0)
                {
                    _rmsIndex--;
                }
                if (_edfIndex != _edf.Length - 1 && _edfIndex != 0 && _rmsIndex == _rms.Length - 1)
                {
                    _edfIndex--;
                }
            }

            if (_rmsIndex == 0)
            {
                tableGen.SetBackGroundColor(4, 0, Color_Transparent);
                tableGen.SetBackGroundColor(5, 0, Color_Transparent);
                tableGen.SetBackGroundColor(6, 0, Color_Transparent);
            }
            if (_edfIndex == 0)
            {
                tableGen.SetBackGroundColor(8, 0, Color_Transparent);
                tableGen.SetBackGroundColor(9, 0, Color_Transparent);
                tableGen.SetBackGroundColor(10, 0, Color_Transparent);
            }

            if (_edfIndex > 0 && _edfIndex <= _edf.Length - 1)
            {
                int currentValue = _edf[_edfIndex];
                int i = _edfIndex;
                while (_edf[i] == currentValue)
                {
                    if (_edf[i] == 0)
                    {
                        tableGen.SetBackGroundColor(8, i, Color_Transparent);
                    }
                    if (_edf[i] == 1)
                    {
                        tableGen.SetBackGroundColor(9, i, Color_Transparent);
                    }
                    if (_edf[i] == 2)
                    {
                        tableGen.SetBackGroundColor(10, i, Color_Transparent);
                    }
                    i--;
                    if (i < 0)
                    {
                        i = 0;
                        break;
                    }
                }
                _edfIndex = i;
                currentColumnOfInterest = _edfIndex + 1;
            }
            else
            {
                if (_rmsIndex > 0 && _rmsIndex <= _rms.Length - 1)
                {
                    int currentValue = _rms[_rmsIndex];
                    int i = _rmsIndex;
                    while (_rms[i] == currentValue)
                    {
                        if (_rms[i] == 0)
                        {
                            tableGen.SetBackGroundColor(4, i, Color_Transparent);
                        }
                        if (_rms[i] == 1)
                        {
                            tableGen.SetBackGroundColor(5, i, Color_Transparent);
                        }
                        if (_rms[i] == 2)
                        {
                            tableGen.SetBackGroundColor(6, i, Color_Transparent);
                        }
                        i--;
                        if (i < 0)
                        {
                            i = 0;
                            break;
                        }
                    }
                    _rmsIndex = i;
                    currentColumnOfInterest = _rmsIndex + 1;
                }
            }



            return tableGen.Grid;
        }

        public Grid GenerateTable_ShowSolution()
        {
            while (_edfIndex < _edf.Length - 1 || _rmsIndex < _rms.Length - 1)
            {
                GenerateTable_NextStep();
            }

            return tableGen.Grid;
        }
    }
}