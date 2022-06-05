using Italbytz.Ports.Exam.OperatingSystems;
using System;
using System.Collections.Generic;
using System.Text;

namespace ISDCompanion.Services.InfoTextServices
{
    internal class PageReplacement_Optiomal_InfoTextService
    {
        public string[] GetInfoTexts(List<IPageReplacementStep> steps)
        {
            string[] infoTexts = new string[steps.Count];
            int index = 0;

            foreach(IPageReplacementStep step in steps)
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
                if(index == 0)
                {
                    inserted_Frame_Was_Empty_Before = true;
                }
                else
                {
                    if (index > 0)
                    {
                        if (steps[index - 1].Frames[frame_Index] > 1000)
                        {
                            inserted_Frame_Was_Empty_Before = true;
                        }
                        else
                        {
                            //Bestimmung, ob das Element bereits vorhanden ist
                            foreach(var i in steps[index-1].Frames)
                            {
                                if (i == element)
                                {
                                    element_Already_Existed = true;
                                    break;
                                }
                            }
                            
                            distance = steps[index - 1].FrameInformation[frame_Index];
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
                    //Kacheln waren voll, nach optimalem Algorithmus wird die Kachel ersetzt,
                    //wessen nächste Nutzung am weitesten in der Zukunft liegt
                    else
                    {
                        if (distance > 1000)
                        {
                            infoText += "da das Element dieser Kachel nicht mehr genutzt wird.";
                        }
                        else
                        {
                            infoText += "da die nächste Nutzung des Elementes dieser Kachel am weitesten (" + distance + ") in der Zukunft lag.";
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
