using Italbytz.Ports.Exam.OperatingSystems;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ISDCompanion.Services
{
    static internal class TableGenService
    {
        /*
        Neue Funktionen für TableGen selber programmieren oder Pascal überlassen --> Klare Trennung?

        -   Vorhande Funktionen auch für Zeilen und Spalten, nicht nur Zellen.
        -   (height, width) ist anders als bei Excel :( --> (width, height)
        -   Layout für Texte anpassen/anpassbar machen: Zentriert
        -   Title Row/Column? 
        */

        public static Grid GenerateTable_PageReplacement_Optimal(List<IPageReplacementStep> steps)
        {
            var tableGen = new TableGen.TableGen(steps.Count, 9, 25, 50);

            tableGen.SetBorderForRow(0);

            tableGen.SetBorderForRow(2);
            tableGen.SetBorderForRow(3);
            tableGen.SetBorderForRow(4);

            tableGen.SetBorderForRow(6);
            tableGen.SetBorderForRow(7);
            tableGen.SetBorderForRow(8);

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

            tableGen.SetColumnWidth(0, 80);

            List<Label> labels = new List<Label>();

            labels.Add(new Label() { Text = "Ref." });
            labels.Add(new Label() { Text = "Kachel 1" });
            labels.Add(new Label() { Text = "Kachel 2" });
            labels.Add(new Label() { Text = "Kachel 3" });
            labels.Add(new Label() { Text = "Abstand 1" });
            labels.Add(new Label() { Text = "Abstand 2" });
            labels.Add(new Label() { Text = "Abstand 3" });

            tableGen.AddElement(0, 0, labels[0]);
            tableGen.AddElement(2, 0, labels[1]);
            tableGen.AddElement(3, 0, labels[2]);
            tableGen.AddElement(4, 0, labels[3]);
            tableGen.AddElement(6, 0, labels[4]);
            tableGen.AddElement(7, 0, labels[5]);
            tableGen.AddElement(8, 0, labels[6]);

            for(int i = 0; i < steps.Count; i++)
            {
                Label label = new Label(){ Text = "1" };
                tableGen.AddElement(1, i, label);
            }

            return tableGen.Grid;
        }
    }
}
