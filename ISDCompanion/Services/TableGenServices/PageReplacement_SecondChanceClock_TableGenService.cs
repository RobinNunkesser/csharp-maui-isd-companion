using Italbytz.Ports.Exam.OperatingSystems;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ISDCompanion.Services
{
    internal class PageReplacement_SecondChanceClock_TableGenService : ITableGenService
    {
        private TableGen.TableGen _tableGen;

        private int _index;
        private int _cellWidth = 50;
        List<IPageReplacementStep> _steps;

        private int currentColumnOfInterest { get; set; }

        public int X_CoordoninatesOfInterest()
        {
            return (currentColumnOfInterest - 3) * _cellWidth;
        }

        public int Y_CoordoninatesOfInterest()
        {
            return 0;
        }

        public PageReplacement_SecondChanceClock_TableGenService(List<IPageReplacementStep> steps)
        {
            _tableGen = new TableGen.TableGen(steps.Count, 13, 25, 50);
            _index = 0;
            currentColumnOfInterest = 0;
            _steps = steps;
        }


        public Grid GenerateTable_TableHeader()
        {
            var tableGen = new TableGen.TableGen(1, 13, 25, 80);

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

            return tableGen.Grid;
        }

        public Grid GenerateTable_EmptyTable()
        {
            _tableGen.SetBorderForRow(0);

            _tableGen.SetBorderForRow(2);
            _tableGen.SetBorderForRow(3);
            _tableGen.SetBorderForRow(4);
            _tableGen.SetBorderForRow(5);

            _tableGen.SetBorderForRow(7);
            _tableGen.SetBorderForRow(8);
            _tableGen.SetBorderForRow(9);
            _tableGen.SetBorderForRow(10);

            _tableGen.SetBorderForRow(12);


            for (int i = 0; i < _steps.Count; i++)
            {
                Label label;
                String element;

                //Reference
                element = _steps[i].Element.ToString();
                label = new Label() { Text = element };
                _tableGen.AddCenteredElement(0, i, label);
            }

            return _tableGen.Grid;
        }

        public Grid GenerateTable_NextStep()
        {
            Label label;
            String element;

            if (_index < _steps.Count)
            {
                //Kachel
                for (int j = 0; j <= 3; j++)
                {
                    element = _steps[_index].Frames[j].ToString();
                    if (element == "2147483647")
                    {
                        element = "∞";
                    }
                    label = new Label() { Text = element };
                    _tableGen.AddCenteredElement(2 + j, _index, label);
                }

                //Abstand
                for (int j = 0; j <= 3; j++)
                {
                    element = _steps[_index].FrameInformation[j].ToString();
                    if (element == "2147483647")
                    {
                        element = "∞";
                    }
                    label = new Label() { Text = element };
                    _tableGen.AddCenteredElement(7 + j, _index, label);
                }

                //Zeiger
                element = _steps[_index].AdditionalInfo.ToString();
                label = new Label() { Text = element };
                _tableGen.AddCenteredElement(12, _index, label);

                _index++;
            }

            return _tableGen.Grid;
        }

        public Grid GenerateTable_PreviousStep()
        {
            if (_index > 0)
            {
                //ToDo

                _index--;
            }
            return _tableGen.Grid;
        }

        public Grid GenerateTable_ShowSolution()
        {
            while (_index < _steps.Count)
            {
                GenerateTable_NextStep();
            }

            return _tableGen.Grid;
        }
    }
}