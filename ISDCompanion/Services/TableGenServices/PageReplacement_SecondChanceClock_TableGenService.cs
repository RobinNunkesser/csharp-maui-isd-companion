﻿using ISDCompanion.Resx;
using ISDCompanion.Services.InfoTextServices;
using ISDCompanion.Services.Interfaces;
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
        private IInfoTextService _infoTextService;

        private int _index;
        private int _cellWidth = 50;
        List<IPageReplacementStep> _steps;

        private int currentColumnOfInterest { get; set; }

        public int X_CoordoninatesOfInterest()
        {
            return (currentColumnOfInterest - 3) * _cellWidth;
        }
        private string[] InfoTexts { get; set; }

        public int Y_CoordoninatesOfInterest()
        {
            return 0;
        }

        public PageReplacement_SecondChanceClock_TableGenService(List<IPageReplacementStep> steps)
        {
            int length = steps.Count;
            _tableGen = new TableGen.TableGen(length, 13, 25, 50);
            _index = 0;
            currentColumnOfInterest = 0;
            _steps = steps;

            _infoTextService = new PageReplacement_SecondChanceClock_InfoTextService(_steps);
            InfoTexts = _infoTextService.GetInfoTexts();
        }


        public Grid GenerateTable_TableHeader()
        {
            TableGen.TableGen tableGen_TableHeader = new TableGen.TableGen(1, 13, 25, 80);

            for (int i = 0; i < 12; i++)
            {
                tableGen_TableHeader.SetBackGroundColor(i, 0, Color.Transparent);
            }

            List<Label> labels = new List<Label>();

            labels.Add(new Label() { Text = AppResources.TableLabel_PageReplacement_Reference });
            labels.Add(new Label() { Text = AppResources.TableLabel_PageReplacement_Slot + " 1" });
            labels.Add(new Label() { Text = AppResources.TableLabel_PageReplacement_Slot + " 2" });
            labels.Add(new Label() { Text = AppResources.TableLabel_PageReplacement_Slot + " 3" });
            labels.Add(new Label() { Text = AppResources.TableLabel_PageReplacement_Slot + " 4" });
            labels.Add(new Label() { Text = AppResources.TableLabel_PageReplacement_Reference + " 1" });
            labels.Add(new Label() { Text = AppResources.TableLabel_PageReplacement_Reference + " 2" });
            labels.Add(new Label() { Text = AppResources.TableLabel_PageReplacement_Reference + " 3" });
            labels.Add(new Label() { Text = AppResources.TableLabel_PageReplacement_Reference + " 4" });
            labels.Add(new Label() { Text = AppResources.TableLabel_PageReplacement_Pointer });

            tableGen_TableHeader.AddElement(0, 0, labels[0]);

            tableGen_TableHeader.AddElement(2, 0, labels[1]);
            tableGen_TableHeader.AddElement(3, 0, labels[2]);
            tableGen_TableHeader.AddElement(4, 0, labels[3]);
            tableGen_TableHeader.AddElement(5, 0, labels[4]);

            tableGen_TableHeader.AddElement(7, 0, labels[5]);
            tableGen_TableHeader.AddElement(8, 0, labels[6]);
            tableGen_TableHeader.AddElement(9, 0, labels[7]);
            tableGen_TableHeader.AddElement(10, 0, labels[8]);

            tableGen_TableHeader.AddElement(12, 0, labels[9]);

            return tableGen_TableHeader.Grid;
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
                currentColumnOfInterest = _index;
            }

            return _tableGen.Grid;
        }

        public Grid GenerateTable_PreviousStep()
        {
            if (_index > 0)
            {
                _index--;

                //Kachel
                for (int j = 0; j <= 3; j++)
                {
                    _tableGen.RemoveElements(2 + j, _index);
                }

                //Abstand
                for (int j = 0; j <= 3; j++)
                {
                    _tableGen.RemoveElements(7 + j, _index);
                }

                //Zeiger
                _tableGen.RemoveElements(12, _index);


                currentColumnOfInterest = _index;

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