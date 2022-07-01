using ISDCompanion.Services.Interfaces;
using Italbytz.Ports.Exam.OperatingSystems;
using System;
using System.Collections.Generic;
using System.Text;

namespace ISDCompanion.Services.InfoTextServices
{
    internal class PageReplacement_FIFO_InfoTextService: IInfoTextService
    {
        List<IPageReplacementStep> _steps;

        public PageReplacement_FIFO_InfoTextService(List<IPageReplacementStep> steps)
        {
            _steps = steps;
        }

        public string[] GetInfoTexts()
        {
            string[] infoTexts = new string[_steps.Count];
            int index = 0;

            foreach (IPageReplacementStep step in _steps)
            {
                string infoText = "";

                int element = step.Element;
                int frame_Index = 0;
                bool inserted_Frame_Was_Empty_Before = false;
                bool element_Already_Existed = false;
                int distance = 0;

                //Bestimmung, welche Kachel verwendet wurde
                foreach (var i in step.Frames)
                {
                    if (i == element)
                    {
                        break;
                    }
                    frame_Index++;
                }

                //Bestimmung, ob die Kachel vorher leer war
                if (index == 0)
                {
                    inserted_Frame_Was_Empty_Before = true;
                }
                else
                {
                    if (index > 0)
                    {
                        if (_steps[index - 1].Frames[frame_Index] > 1000)
                        {
                            inserted_Frame_Was_Empty_Before = true;
                        }
                        else
                        {
                            //Bestimmung, ob das Element bereits vorhanden ist
                            foreach (var i in _steps[index - 1].Frames)
                            {
                                if (i == element)
                                {
                                    element_Already_Existed = true;
                                    break;
                                }
                            }

                            distance = _steps[index - 1].FrameInformation[frame_Index];
                        }
                    }
                }

                if (element_Already_Existed)
                {
                    infoText += "Das Element ist bereits in Kachel " + (frame_Index + 1) + " vorhanden.";
                }
                else
                {
                    infoText += "Das Element " + element + " wurde in Kachel " + (frame_Index + 1) + " eingefügt, ";

                    //Kachel war leer, Element wurde eingefügt
                    if (inserted_Frame_Was_Empty_Before)
                    {
                        infoText += "da die Kachel leer stand.";
                    }
                    //Kacheln waren voll, nach FIFO Algorithmus wird die Kachel ersetzt,
                    //wessen nächste Nutzung am weitesten in der Vergangenheit liegt
                    else
                    {
                        if (distance > 1000)
                        {
                            infoText += "da das Element dieser Kachel nicht mehr genutzt wird.";
                        }
                        else
                        {
                            infoText += "da die letzte Nutzung des Elementes dieser Kachel am weitesten (" + distance + ") in der Vergangenheit lag.";
                        }
                    }
                }

                infoTexts[index] = infoText;
                index++;
            }

            return infoTexts;
        }
    }
}
