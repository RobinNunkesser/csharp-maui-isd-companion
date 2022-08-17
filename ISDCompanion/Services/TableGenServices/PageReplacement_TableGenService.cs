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
    internal class PageReplacement_TableGenService : ITableGenService
    {
        public enum Algorithm { Optimal, FIFO, LRU }

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

        public int Y_CoordoninatesOfInterest()
        {
            return 0;
        }

        private string[] InfoTexts { get; set; }


        public PageReplacement_TableGenService(List<IPageReplacementStep> steps, Algorithm algorithm)
        {
            int length = steps.Count;
            _tableGen = new TableGen.TableGen(length, 11, 25, 50);
            _index = 0;
            currentColumnOfInterest = 0;
            _steps = steps;

            switch (algorithm)
            {
                case Algorithm.Optimal:

                    _infoTextService = new PageReplacement_Optimal_InfoTextService(_steps);
                    break;

                case Algorithm.FIFO:
                    _infoTextService = new PageReplacement_FIFO_InfoTextService(_steps);
                    break;

                case Algorithm.LRU:
                    _infoTextService = new PageReplacement_LRU_InfoTextService(_steps);
                    break;
            }

            InfoTexts = _infoTextService.GetInfoTexts();
        }


        public Grid GenerateTable_TableHeader()
        {
            TableGen.TableGen tableGen_TableHeader = new TableGen.TableGen(1, 11, 25, 80);

            for (int i = 0; i < 10; i++)
            {
                tableGen_TableHeader.SetBackGroundColor(i, 0, Color.Transparent);
            }

            List<Label> labels = new List<Label>();

            labels.Add(new Label() { Text = AppResources.TableLabel_PageReplacement_Reference });
            labels.Add(new Label() { Text = AppResources.TableLabel_PageReplacement_Slot + " 1" });
            labels.Add(new Label() { Text = AppResources.TableLabel_PageReplacement_Slot + " 2" });
            labels.Add(new Label() { Text = AppResources.TableLabel_PageReplacement_Slot + " 3" });
            labels.Add(new Label() { Text = AppResources.TableLabel_PageReplacement_Slot + " 4" });
            labels.Add(new Label() { Text = AppResources.TableLabel_PageReplacement_Distance + " 1" });
            labels.Add(new Label() { Text = AppResources.TableLabel_PageReplacement_Distance + " 2" });
            labels.Add(new Label() { Text = AppResources.TableLabel_PageReplacement_Distance + " 3" });
            labels.Add(new Label() { Text = AppResources.TableLabel_PageReplacement_Distance + " 4" });

            tableGen_TableHeader.AddElement(0, 0, labels[0]);

            tableGen_TableHeader.AddElement(2, 0, labels[1]);
            tableGen_TableHeader.AddElement(3, 0, labels[2]);
            tableGen_TableHeader.AddElement(4, 0, labels[3]);
            tableGen_TableHeader.AddElement(5, 0, labels[4]);

            tableGen_TableHeader.AddElement(7, 0, labels[5]);
            tableGen_TableHeader.AddElement(8, 0, labels[6]);
            tableGen_TableHeader.AddElement(9, 0, labels[7]);
            tableGen_TableHeader.AddElement(10, 0, labels[8]);

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
            if(_index == 0)
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