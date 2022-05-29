using Italbytz.Adapters.Exam.OperatingSystems;
using Italbytz.Ports.Exam.OperatingSystems;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ISDCompanion.Services
{
    static internal class TableGenService
    {
        #region PageReplacement

        public enum PageReplacementAlgorithm
        {
            Optimal,
            FIFO,
            LRU,
            SecondChance
        }

        public static Grid GenerateTable_PageReplacement(List<IPageReplacementStep> steps, PageReplacementAlgorithm algorithm)
        {
            if (algorithm == PageReplacementAlgorithm.SecondChance)
            {
                return GenerateTable_PageReplacement_SecondChanceClock(steps);
            }
            else
            {
                return GenerateTable_PageReplacement(steps);
            }
        }

        private static Grid GenerateTable_PageReplacement(List<IPageReplacementStep> steps)
        {
            var tableGen = new TableGen.TableGen(steps.Count + 1, 11, 25, 50);

            tableGen.SetBorderForRow(0);

            tableGen.SetBorderForRow(2);
            tableGen.SetBorderForRow(3);
            tableGen.SetBorderForRow(4);
            tableGen.SetBorderForRow(5);

            tableGen.SetBorderForRow(7);
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

            List<Label> labels = new List<Label>();

            labels.Add(new Label() { Text = "Ref." });
            labels.Add(new Label() { Text = "Kachel 1" });
            labels.Add(new Label() { Text = "Kachel 2" });
            labels.Add(new Label() { Text = "Kachel 3" });
            labels.Add(new Label() { Text = "Kachel 4" });
            labels.Add(new Label() { Text = "Abstand 1" });
            labels.Add(new Label() { Text = "Abstand 2" });
            labels.Add(new Label() { Text = "Abstand 3" });
            labels.Add(new Label() { Text = "Abstand 4" });

            tableGen.AddElement(0, 0, labels[0]);
            tableGen.AddElement(2, 0, labels[1]);
            tableGen.AddElement(3, 0, labels[2]);
            tableGen.AddElement(4, 0, labels[3]);
            tableGen.AddElement(5, 0, labels[4]);

            tableGen.AddElement(7, 0, labels[5]);
            tableGen.AddElement(8, 0, labels[6]);
            tableGen.AddElement(9, 0, labels[7]);
            tableGen.AddElement(10, 0, labels[8]);

            for (int i = 0; i < steps.Count; i++)
            {
                Label label;
                String element;

                //Reference
                element = steps[i].Element.ToString();
                label = new Label() { Text = element };
                tableGen.AddCenteredElement(0, i + 1, label);

                //Kachel
                for (int j = 0; j <= 3; j++)
                {
                    element = steps[i].Frames[j].ToString();
                    if (element == "2147483647")
                    {
                        element = "";
                    }
                    label = new Label() { Text = element };
                    tableGen.AddCenteredElement(2 + j, i + 1, label);
                }

                //Abstand
                for (int j = 0; j <= 3; j++)
                {
                    element = steps[i].FrameInformation[j].ToString();
                    if (element == "2147483647")
                    {
                        element = "";
                    }
                    label = new Label() { Text = element };
                    tableGen.AddCenteredElement(7 + j, i + 1, label);
                }
            }
            return tableGen.Grid;
        }

        public static Grid GenerateTable_PageReplacement_SecondChanceClock(List<IPageReplacementStep> steps)
        {
            var tableGen = new TableGen.TableGen(steps.Count + 1, 13, 25, 50);

            tableGen.SetBorderForRow(0);

            tableGen.SetBorderForRow(2);
            tableGen.SetBorderForRow(3);
            tableGen.SetBorderForRow(4);
            tableGen.SetBorderForRow(5);

            tableGen.SetBorderForRow(7);
            tableGen.SetBorderForRow(8);
            tableGen.SetBorderForRow(9);
            tableGen.SetBorderForRow(10);

            tableGen.SetBorderForRow(12);

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

            tableGen.RemoveBorder(11, 0, TableGen.Border.BorderPosition.Top);
            tableGen.RemoveBorder(11, 0, TableGen.Border.BorderPosition.Left);
            tableGen.RemoveBorder(11, 0, TableGen.Border.BorderPosition.Bot);

            tableGen.RemoveBorder(12, 0, TableGen.Border.BorderPosition.Top);
            tableGen.RemoveBorder(12, 0, TableGen.Border.BorderPosition.Left);
            tableGen.RemoveBorder(12, 0, TableGen.Border.BorderPosition.Bot);

            tableGen.SetColumnWidth(0, 80);

            List<Label> labels = new List<Label>();

            labels.Add(new Label() { Text = "Ref." });
            labels.Add(new Label() { Text = "Kachel 1" });
            labels.Add(new Label() { Text = "Kachel 2" });
            labels.Add(new Label() { Text = "Kachel 3" });
            labels.Add(new Label() { Text = "Kachel 4" });
            labels.Add(new Label() { Text = "Abstand 1" });
            labels.Add(new Label() { Text = "Abstand 2" });
            labels.Add(new Label() { Text = "Abstand 3" });
            labels.Add(new Label() { Text = "Abstand 4" });
            labels.Add(new Label() { Text = "Zeiger" });

            tableGen.AddElement(0, 0, labels[0]);
            tableGen.AddElement(2, 0, labels[1]);
            tableGen.AddElement(3, 0, labels[2]);
            tableGen.AddElement(4, 0, labels[3]);
            tableGen.AddElement(5, 0, labels[4]);

            tableGen.AddElement(7, 0, labels[5]);
            tableGen.AddElement(8, 0, labels[6]);
            tableGen.AddElement(9, 0, labels[7]);
            tableGen.AddElement(10, 0, labels[8]);

            tableGen.AddElement(12, 0, labels[9]);

            for (int i = 0; i < steps.Count; i++)
            {
                Label label;
                String element;

                //Reference
                element = steps[i].Element.ToString();
                label = new Label() { Text = element };
                tableGen.AddCenteredElement(0, i + 1, label);

                //Kachel
                for (int j = 0; j <= 3; j++)
                {
                    element = steps[i].Frames[j].ToString();
                    if (element == "2147483647")
                    {
                        element = "";
                    }
                    label = new Label() { Text = element };
                    tableGen.AddCenteredElement(2 + j, i + 1, label);
                }

                //Abstand
                for (int j = 0; j <= 3; j++)
                {
                    element = steps[i].FrameInformation[j].ToString();
                    if (element == "2147483647")
                    {
                        element = "";
                    }
                    label = new Label() { Text = element };
                    tableGen.AddCenteredElement(7 + j, i + 1, label);
                }

                //Zeiger
                element = steps[i].AdditionalInfo.ToString();
                label = new Label() { Text = element };
                tableGen.AddCenteredElement(12, i + 1, label);

            }

            return tableGen.Grid;
        }

        #endregion

        #region RealtimeScheduling

        public static Grid GenerateTable_RealtimeScheduling_Complete(IRealtimeSchedulingParameters parameters, int[] edf, int[] rms)
        {
            var tableGen = new TableGen.TableGen(33, 11, 25, 25);

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

            Color Color_A = Color.FromRgb(200, 0, 0);
            Color Color_B = Color.FromRgb(0, 0, 200);
            Color Color_C = Color.FromRgb(0, 200, 0);

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

        public static Grid GenerateTable_RealtimeScheduling_Table(IRealtimeSchedulingParameters parameters, int[] edf, int[] rms)
        {
            var tableGen = new TableGen.TableGen(33, 11, 25, 25);

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

            Color Color_A = Color.FromRgb(200, 0, 0);
            Color Color_B = Color.FromRgb(0, 0, 200);
            Color Color_C = Color.FromRgb(0, 200, 0);

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

            return tableGen.Grid;
        }

        private static int[] edf;
        private static int edfIndex;

        //public static Grid NextStep_RealtimeScheduling_EDF(Grid grid)
        //{
        //    //for (int i = 0; i < edf.Length; i++)
        //    //{
        //    //    if (edf[i] == 0)
        //    //    {
        //    //        tableGen.SetBackGroundColor(8, i + 1, Color_A);
        //    //    }
        //    //    if (edf[i] == 1)
        //    //    {
        //    //        tableGen.SetBackGroundColor(9, i + 1, Color_B);
        //    //    }
        //    //    if (edf[i] == 2)
        //    //    {
        //    //        tableGen.SetBackGroundColor(10, i + 1, Color_C);
        //    //    }
        //    //}

        //}

        private static int[] rms;
        private static int rmsIndex;



        #endregion

        #region Buddy

        public static Grid GenerateTable_Buddy(BuddyParameters parameters, IBuddySolution solution)
        {
            var tableGen = new TableGen.TableGen(33, 10, 25, 25);

            tableGen.SetBorderForRow(0);
            tableGen.SetBorderForRow(1);
            tableGen.SetBorderForRow(2);
            tableGen.SetBorderForRow(3);
            tableGen.SetBorderForRow(4);
            tableGen.SetBorderForRow(5);
            tableGen.SetBorderForRow(6);
            tableGen.SetBorderForRow(7);
            tableGen.SetBorderForRow(8);
            tableGen.SetBorderForRow(9);
            //tableGen.SetBorderForRow(10);

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

            //tableGen.RemoveBorder(10, 0, TableGen.Border.BorderPosition.Top);
            //tableGen.RemoveBorder(10, 0, TableGen.Border.BorderPosition.Left);
            //tableGen.RemoveBorder(10, 0, TableGen.Border.BorderPosition.Bot);

            tableGen.SetColumnWidth(0, 80);

            List<Label> labels = new List<Label>();

            labels.Add(new Label() { Text = parameters.Processes[0] + " (" + parameters.Requests[0] + ")" });
            labels.Add(new Label() { Text = parameters.Processes[1] + " (" + parameters.Requests[1] + ")" });
            labels.Add(new Label() { Text = parameters.Processes[2] + " (" + parameters.Requests[2] + ")" });
            labels.Add(new Label() { Text = parameters.Processes[3] + " (" + parameters.Requests[3] + ")" });
            labels.Add(new Label() { Text = parameters.Processes[4] + " (" + parameters.Requests[4] + ")" });
            labels.Add(new Label() { Text = "Free " + parameters.FreeOrder[0] });
            labels.Add(new Label() { Text = "Free " + parameters.FreeOrder[1] });
            labels.Add(new Label() { Text = "Free " + parameters.FreeOrder[2] });
            labels.Add(new Label() { Text = "Free " + parameters.FreeOrder[3] });
            labels.Add(new Label() { Text = "Free " + parameters.FreeOrder[4] });

            tableGen.AddElement(0, 0, labels[0]);
            tableGen.AddElement(1, 0, labels[1]);
            tableGen.AddElement(2, 0, labels[2]);
            tableGen.AddElement(3, 0, labels[3]);
            tableGen.AddElement(4, 0, labels[4]);
            tableGen.AddElement(5, 0, labels[5]);
            tableGen.AddElement(6, 0, labels[6]);
            tableGen.AddElement(7, 0, labels[7]);
            tableGen.AddElement(8, 0, labels[8]);
            tableGen.AddElement(9, 0, labels[9]);
                       

            for(int i = 0; i < solution.History.Count; i++)
            {
                for(int j = 0; j < solution.History[i].Length; j++)
                {
                    string letter= solution.History[i][j] == -1 ? "" : parameters.Processes[solution.History[i][j]];
                    tableGen.AddCenteredElement(i, j + 1, new Label() { Text = letter });
                }
            }

            return tableGen.Grid;
        }

        #endregion
    }
}
