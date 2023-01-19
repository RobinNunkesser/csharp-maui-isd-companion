using StudyCompanion.Resources.Strings;
using StudyCompanion.Services.Interfaces;
using Italbytz.Ports.Exam.OperatingSystems;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyCompanion.Services.InfoTextServices
{
    internal class PageReplacement_FIFO_InfoTextService : IInfoTextService
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
                    infoText = String.Format(AppResources.InfoText_PageReplacement_Element_Exists, frame_Index + 1);
                }
                else
                {
                    infoText = String.Format(AppResources.InfoText_PageReplacement_Element_Added, element, frame_Index + 1);

                    //Kachel war leer, Element wurde eingefügt
                    if (inserted_Frame_Was_Empty_Before)
                    {
                        infoText = String.Format(AppResources.InfoText_PageReplacement_Element_Added_Empty);
                    }
                    //Kacheln waren voll, nach FIFO Algorithmus wird die Kachel ersetzt,
                    //wessen nächste Nutzung am weitesten in der Vergangenheit liegt
                    else
                    {
                        if (distance > 1000)
                        {
                            infoText = String.Format(AppResources.InfoText_PageReplacement_Element_Added_NotInUse);
                        }
                        else
                        {
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
