using Italbytz.Ports.Exam.OperatingSystems;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ISDCompanion.Services
{
    internal class RealtimeScheduling_TableGenService
    {
        private TableGen.TableGen tableGen;

        private int[] _edf;
        private int edfIndex;
        private int[] _rms;
        private int rmsIndex;

        public int currentColumnOfInterest { get; private set; }

        Color Color_A = Color.FromRgb(200, 0, 0);
        Color Color_B = Color.FromRgb(0, 0, 200);
        Color Color_C = Color.FromRgb(0, 200, 0);

        public RealtimeScheduling_TableGenService()
        {
             tableGen = new TableGen.TableGen(33, 11, 25, 25);
            edfIndex = 0;
            rmsIndex = 0;
            currentColumnOfInterest = 0;
        }

        public Grid GenerateTable_RealtimeScheduling_Complete(IRealtimeSchedulingParameters parameters, int[] edf, int[] rms)
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

            tableGen.RemoveBorder(0, 0, TableGen.Border.BorderPosition.Top);
            tableGen.RemoveBorder(0, 0, TableGen.Border.BorderPosition.Left);
            tableGen.RemoveBorder(0, 0, TableGen.Border.BorderPosition.Bot);

            tableGen.RemoveBorder(1, 0, TableGen.Border.BorderPosition.Top);
            tableGen.RemoveBorder(1, 0, TableGen.Border.BorderPosition.Left);
            tableGen.RemoveBorder(1, 0, TableGen.Border.BorderPosition.Bot);

            tableGen.RemoveBorder(2, 0, TableGen.Border.BorderPosition.Top);
            tableGen.RemoveBorder(2, 0, TableGen.Border.BorderPosition.Left);
            tableGen.RemoveBorder(2, 0, TableGen.Border.BorderPosition.Bot);

            tableGen.RemoveBorder(3, 0, TableGen.Border.BorderPosition.Top);
            tableGen.RemoveBorder(3, 0, TableGen.Border.BorderPosition.Left);
            tableGen.RemoveBorder(3, 0, TableGen.Border.BorderPosition.Bot);

            tableGen.RemoveBorder(4, 0, TableGen.Border.BorderPosition.Top);
            tableGen.RemoveBorder(4, 0, TableGen.Border.BorderPosition.Left);
            tableGen.RemoveBorder(4, 0, TableGen.Border.BorderPosition.Bot);

            tableGen.RemoveBorder(5, 0, TableGen.Border.BorderPosition.Top);
            tableGen.RemoveBorder(5, 0, TableGen.Border.BorderPosition.Left);
            tableGen.RemoveBorder(5, 0, TableGen.Border.BorderPosition.Bot);

            tableGen.RemoveBorder(6, 0, TableGen.Border.BorderPosition.Top);
            tableGen.RemoveBorder(6, 0, TableGen.Border.BorderPosition.Left);
            tableGen.RemoveBorder(6, 0, TableGen.Border.BorderPosition.Bot);

            tableGen.RemoveBorder(7, 0, TableGen.Border.BorderPosition.Top);
            tableGen.RemoveBorder(7, 0, TableGen.Border.BorderPosition.Left);
            tableGen.RemoveBorder(7, 0, TableGen.Border.BorderPosition.Bot);

            tableGen.RemoveBorder(8, 0, TableGen.Border.BorderPosition.Top);
            tableGen.RemoveBorder(8, 0, TableGen.Border.BorderPosition.Left);
            tableGen.RemoveBorder(8, 0, TableGen.Border.BorderPosition.Bot);

            tableGen.RemoveBorder(9, 0, TableGen.Border.BorderPosition.Top);
            tableGen.RemoveBorder(9, 0, TableGen.Border.BorderPosition.Left);
            tableGen.RemoveBorder(9, 0, TableGen.Border.BorderPosition.Bot);

            tableGen.RemoveBorder(10, 0, TableGen.Border.BorderPosition.Top);
            tableGen.RemoveBorder(10, 0, TableGen.Border.BorderPosition.Left);
            tableGen.RemoveBorder(10, 0, TableGen.Border.BorderPosition.Bot);

            tableGen.SetColumnWidth(0, 80);


            tableGen.SetRowHeight(3, 10);
            tableGen.SetRowHeight(7, 10);

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

            tableGen.AddElement(0, 0, labels[0]);
            tableGen.AddElement(1, 0, labels[1]);
            tableGen.AddElement(2, 0, labels[2]);

            tableGen.AddElement(4, 0, labels[3]);
            tableGen.AddElement(5, 0, labels[4]);
            tableGen.AddElement(6, 0, labels[5]);

            tableGen.AddElement(8, 0, labels[6]);
            tableGen.AddElement(9, 0, labels[7]);
            tableGen.AddElement(10, 0, labels[8]);

            int process = 0;
            foreach (System.ValueTuple<int, int> request in parameters.Requests)
            {
                int index = 0;
                while (index < 32)
                {
                    for (int i = 0; i < request.Item1; i++)
                    {
                        int j = index + 1 + i;
                        if (j <= 32)
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

            for (int i = 0; i < rms.Length; i++)
            {
                if (rms[i] == 0)
                {
                    tableGen.SetBackGroundColor(4, i + 1, Color_A);
                }
                if (rms[i] == 1)
                {
                    tableGen.SetBackGroundColor(5, i + 1, Color_B);
                }
                if (rms[i] == 2)
                {
                    tableGen.SetBackGroundColor(6, i + 1, Color_C);
                }
            }

            for (int i = 0; i < edf.Length; i++)
            {
                if (edf[i] == 0)
                {
                    tableGen.SetBackGroundColor(8, i + 1, Color_A);
                }
                if (edf[i] == 1)
                {
                    tableGen.SetBackGroundColor(9, i + 1, Color_B);
                }
                if (edf[i] == 2)
                {
                    tableGen.SetBackGroundColor(10, i + 1, Color_C);
                }
            }

            return tableGen.Grid;
        }

        public Grid GenerateTable_RealtimeScheduling_EmptyTable(IRealtimeSchedulingParameters parameters, int[] edf, int[] rms)
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

            tableGen.RemoveBorder(0, 0, TableGen.Border.BorderPosition.Top);
            tableGen.RemoveBorder(0, 0, TableGen.Border.BorderPosition.Left);
            tableGen.RemoveBorder(0, 0, TableGen.Border.BorderPosition.Bot);

            tableGen.RemoveBorder(1, 0, TableGen.Border.BorderPosition.Top);
            tableGen.RemoveBorder(1, 0, TableGen.Border.BorderPosition.Left);
            tableGen.RemoveBorder(1, 0, TableGen.Border.BorderPosition.Bot);

            tableGen.RemoveBorder(2, 0, TableGen.Border.BorderPosition.Top);
            tableGen.RemoveBorder(2, 0, TableGen.Border.BorderPosition.Left);
            tableGen.RemoveBorder(2, 0, TableGen.Border.BorderPosition.Bot);

            tableGen.RemoveBorder(3, 0, TableGen.Border.BorderPosition.Top);
            tableGen.RemoveBorder(3, 0, TableGen.Border.BorderPosition.Left);
            tableGen.RemoveBorder(3, 0, TableGen.Border.BorderPosition.Bot);

            tableGen.RemoveBorder(4, 0, TableGen.Border.BorderPosition.Top);
            tableGen.RemoveBorder(4, 0, TableGen.Border.BorderPosition.Left);
            tableGen.RemoveBorder(4, 0, TableGen.Border.BorderPosition.Bot);

            tableGen.RemoveBorder(5, 0, TableGen.Border.BorderPosition.Top);
            tableGen.RemoveBorder(5, 0, TableGen.Border.BorderPosition.Left);
            tableGen.RemoveBorder(5, 0, TableGen.Border.BorderPosition.Bot);

            tableGen.RemoveBorder(6, 0, TableGen.Border.BorderPosition.Top);
            tableGen.RemoveBorder(6, 0, TableGen.Border.BorderPosition.Left);
            tableGen.RemoveBorder(6, 0, TableGen.Border.BorderPosition.Bot);

            tableGen.RemoveBorder(7, 0, TableGen.Border.BorderPosition.Top);
            tableGen.RemoveBorder(7, 0, TableGen.Border.BorderPosition.Left);
            tableGen.RemoveBorder(7, 0, TableGen.Border.BorderPosition.Bot);

            tableGen.RemoveBorder(8, 0, TableGen.Border.BorderPosition.Top);
            tableGen.RemoveBorder(8, 0, TableGen.Border.BorderPosition.Left);
            tableGen.RemoveBorder(8, 0, TableGen.Border.BorderPosition.Bot);

            tableGen.RemoveBorder(9, 0, TableGen.Border.BorderPosition.Top);
            tableGen.RemoveBorder(9, 0, TableGen.Border.BorderPosition.Left);
            tableGen.RemoveBorder(9, 0, TableGen.Border.BorderPosition.Bot);

            tableGen.RemoveBorder(10, 0, TableGen.Border.BorderPosition.Top);
            tableGen.RemoveBorder(10, 0, TableGen.Border.BorderPosition.Left);
            tableGen.RemoveBorder(10, 0, TableGen.Border.BorderPosition.Bot);

            tableGen.SetColumnWidth(0, 80);


            tableGen.SetRowHeight(3, 10);
            tableGen.SetRowHeight(7, 10);

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

            tableGen.AddElement(0, 0, labels[0]);
            tableGen.AddElement(1, 0, labels[1]);
            tableGen.AddElement(2, 0, labels[2]);

            tableGen.AddElement(4, 0, labels[3]);
            tableGen.AddElement(5, 0, labels[4]);
            tableGen.AddElement(6, 0, labels[5]);

            tableGen.AddElement(8, 0, labels[6]);
            tableGen.AddElement(9, 0, labels[7]);
            tableGen.AddElement(10, 0, labels[8]);

            int process = 0;
            foreach (System.ValueTuple<int, int> request in parameters.Requests)
            {
                int index = 0;
                while (index < 32)
                {
                    for (int i = 0; i < request.Item1; i++)
                    {
                        int j = index + 1 + i;
                        if (j <= 32)
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

            //for (int i = 0; i < rms.Length; i++)
            //{
            //    if (rms[i] == 0)
            //    {
            //        tableGen.SetBackGroundColor(4, i + 1, Color_A);
            //    }
            //    if (rms[i] == 1)
            //    {
            //        tableGen.SetBackGroundColor(5, i + 1, Color_B);
            //    }
            //    if (rms[i] == 2)
            //    {
            //        tableGen.SetBackGroundColor(6, i + 1, Color_C);
            //    }
            //}

            //for (int i = 0; i < edf.Length; i++)
            //{
            //    if (edf[i] == 0)
            //    {
            //        tableGen.SetBackGroundColor(8, i + 1, Color_A);
            //    }
            //    if (edf[i] == 1)
            //    {
            //        tableGen.SetBackGroundColor(9, i + 1, Color_B);
            //    }
            //    if (edf[i] == 2)
            //    {
            //        tableGen.SetBackGroundColor(10, i + 1, Color_C);
            //    }
            //}

            _edf = edf;
            _rms = rms;

            return tableGen.Grid;
        }

        public Grid NextStep_RealtimeScheduling()
        {
            if(rmsIndex <= _rms.Length - 1)
            {
                int currentValue = _rms[rmsIndex];
                int i = rmsIndex;
                while (_rms[i] == currentValue)
                {
                    if (_rms[i] == 0)
                    {
                        tableGen.SetBackGroundColor(4, i + 1, Color_A);
                    }
                    if (_rms[i] == 1)
                    {
                        tableGen.SetBackGroundColor(5, i + 1, Color_B);
                    }
                    if (_rms[i] == 2)
                    {
                        tableGen.SetBackGroundColor(6, i + 1, Color_C);
                    }
                    i++;
                    if(i > _rms.Length - 1)
                    {
                        break;
                    }
                }
                rmsIndex = i;
                currentColumnOfInterest = rmsIndex;
            }
            else
            {
                if (edfIndex <= _edf.Length - 1)
                {
                    int currentValue = _edf[edfIndex];
                    int i = edfIndex;
                    while (_edf[i] == currentValue)
                    {
                        if (_edf[i] == 0)
                        {
                            tableGen.SetBackGroundColor(8, i + 1, Color_A);
                        }
                        if (_edf[i] == 1)
                        {
                            tableGen.SetBackGroundColor(9, i + 1, Color_B);
                        }
                        if (_edf[i] == 2)
                        {
                            tableGen.SetBackGroundColor(10, i + 1, Color_C);
                        }
                        i++; 
                        if (i > _edf.Length - 1)
                        {
                            break;
                        }
                    }
                    edfIndex = i;
                    currentColumnOfInterest =edfIndex;
                }
            }
            return tableGen.Grid;
        }
    }
}
