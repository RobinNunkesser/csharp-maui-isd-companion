using ISDCompanion.Services.Interfaces;
using Italbytz.Ports.Exam.OperatingSystems;
using System;
using System.Collections.Generic;
using System.Text;

namespace ISDCompanion.Services.InfoTextServices
{
    internal class PageReplacement_SecondChanceClock_InfoTextService : IInfoTextService
    {
        List<IPageReplacementStep> _steps;

        public PageReplacement_SecondChanceClock_InfoTextService(List<IPageReplacementStep> steps)
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
                bool element_Was_Inserted_According_To_Pointer = false;
                int reference = 0;
                int previous_reference = 0;

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

                            reference = Convert.ToInt32(_steps[index].AdditionalInfo);
                            previous_reference = Convert.ToInt32(_steps[index - 1].AdditionalInfo);

                            if (previous_reference == (frame_Index + 1))
                            {
                                element_Was_Inserted_According_To_Pointer = true;
                            }
                        }
                    }
                }

                if (element_Already_Existed)
                {
                    infoText += "Das Element ist bereits in Kachel " + (frame_Index + 1) + " vorhanden.";
                }
                else
                {
                    infoText += "Das Element " + element + " wurde in Kachel " + (frame_Index + 1) + " eingefügt. \n\n";

                    //Kachel war leer, Element wurde eingefügt
                    if (inserted_Frame_Was_Empty_Before)
                    {
                        infoText += "da die Kachel leer stand.";
                    }
                    //Kacheln waren voll, nach Second Chance Clock Algorithmus wird die Kachel ersetzt,
                    //auf welche der Zeiger zeigt. Ist das Referenzbit der Kachel = 1, so wird es 0 gesetzt, und
                    //die nächste Kachel wird betrachtet.
                    else
                    {
                        if (element_Was_Inserted_According_To_Pointer)
                        {
                            infoText += "Der Zeiger bestimmt, welche Kachel zu erst betrachtet wird: Kachel " + previous_reference + ". \n\n" + "Das das Referenzbit dieser Kachel = 0 war, konnte das Element eingesetzt werden. \n\n Nun zeigt der Zeiger auf: " + "Kachel " + reference;
                        }
                        else
                        {
                            infoText += "Der Zeiger bestimmt, welche Kachel zu erst betrachtet wird: " + "Kachel " + previous_reference + ". \n\n" + "Da das Referenzbit dieser Kachel = 1 war, wird dieses zunächst auf 0 gesetzt und anschließend die nächste Kachel betrachtet. \n\n" + "Kachel " + (frame_Index + 1) + " war die erste Kachel mit Referenzbit = 0, in welches das Element eingesetzt werden konnte. \n\n" + "Nun zeigt der Zeiger auf: " + "Kachel " + reference;
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
