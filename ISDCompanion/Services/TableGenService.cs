using Italbytz.Ports.Exam.OperatingSystems;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ISDCompanion.Services
{
    static internal class TableGenService
    {
        public enum PageReplacementAlgorithm
        {
            Optimal,
            FIFO,
            LRU,
            SecondChance
        }

        public static Grid GenerateTable_PageReplacement(List<IPageReplacementStep> steps , PageReplacementAlgorithm algorithm)
        {
            if(algorithm == PageReplacementAlgorithm.SecondChance)
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
    }
}
