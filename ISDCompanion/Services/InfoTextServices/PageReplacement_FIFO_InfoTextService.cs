using ISDCompanion.Resx;
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
                    //InfoText_PageReplacement_Fifo_Element_Exists
                    //Das Element ist bereits in Kachel {0} vorhanden.
                    //The element is already present in Slot {0}.

                    infoText = String.Format(AppResources.InfoText_PageReplacement_Fifo_Element_Exists, frame_Index + 1);
                }
                else
                {
                    //InfoText_PageReplacement_Fifo_Element_Added
                    //Das Element {0} wurde in Kachel {1} eingefügt, 
                    //The element {0} was added in Slot {1}, 

                    infoText = String.Format(AppResources.InfoText_PageReplacement_Fifo_Element_Added, element, frame_Index + 1);

                    //Kachel war leer, Element wurde eingefügt
                    if (inserted_Frame_Was_Empty_Before)
                    {
                        //InfoText_PageReplacement_Fifo_Element_Added_Empty
                        //da die Kachel leer stand.
                        //because the Slot was empty.

                        infoText = String.Format(AppResources.InfoText_PageReplacement_Fifo_Element_Added_Empty);
                    }
                    //Kacheln waren voll, nach FIFO Algorithmus wird die Kachel ersetzt,
                    //wessen nächste Nutzung am weitesten in der Vergangenheit liegt
                    else
                    {
                        if (distance > 1000)
                        {
                            //InfoText_PageReplacement_Fifo_Element_Added_NotInUse
                            //da das Element dieser Kachel nicht mehr genutzt wird.
                            //because the element contained in the Slot is not used anymore.

                            infoText = String.Format(AppResources.InfoText_PageReplacement_Fifo_Element_Added_NotInUse);
                        }
                        else
                        {
                            //InfoText_PageReplacement_Fifo_Element_Added_Distance
                            //da die letzte Nutzung des Elementes dieser Kachel am weitesten ({0}) in der Vergangenheit lag.
                            //because the last use of the element contained in the Slot is the furthest ({0}) in the past.

                            infoText = String.Format(AppResources.InfoText_PageReplacement_Fifo_Element_Added_Distance, distance);
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
