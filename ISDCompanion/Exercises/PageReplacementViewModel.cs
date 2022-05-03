using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Italbytz.Adapters.Exam.OperatingSystems;
using Italbytz.Infrastructure.Exam.OperatingSystems.PageReplacement;
using Italbytz.Ports.Exam.OperatingSystems;
using TableGen;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class PageReplacementViewModel : ExerciseViewModel
    {

        public List<string[]> Items { get; set; }
        private string referenceRequests;
        public string ReferenceRequests
        {
            get => referenceRequests;
            set
            {
                if (value != referenceRequests)
                {
                    referenceRequests = value;
                    OnPropertyChanged();
                }
            }
        }

        private int selectedStrategy;
        public int SelectedStrategy
        {
            get => selectedStrategy;
            set
            {
                if (selectedStrategy != value)
                {
                    selectedStrategy = value;
                    OnPropertyChanged();
                    ComputeItems();
                }
            }
        }

        private List<IPageReplacementStep> clockSolution;
        private List<IPageReplacementStep> optimalSolution;
        private List<IPageReplacementStep> lruSolution;
        private List<IPageReplacementStep> fifoSolution;

        private void ComputeItems()
        {
            Items = new List<string[]>();

            List<IPageReplacementStep> solution = null;
            switch (selectedStrategy)
            {
                case 0: solution = optimalSolution; break;
                case 1: solution = fifoSolution; break;
                case 2: solution = lruSolution; break;
                case 3: solution = clockSolution; break;
            }

            if (solution != null)
            {
                Items = solution.Select(Present).ToList();
            }
            OnPropertyChanged("Items");
        }

        private string[] Present(IPageReplacementStep sim)
        {
            var element = sim.Element;
            var frames = sim.Frames.Select(Present).ToList();
            var frameInformation = sim.FrameInformation.Select(Present).ToList();
            return new string[] {
        $"{element}",
        $"{frames[0]} ({frameInformation[0]})",
        $"{frames[1]} ({frameInformation[1]})",
        $"{frames[2]} ({frameInformation[2]})",
        $"{frames[3]} ({frameInformation[3]})"};
        }

        private string Present(int arg)
        {
            if (arg == int.MaxValue) return "-";//"∞";
            return $"{arg}";
        }

        protected override void Initialize()
        {

            SelectedStrategy = -1;

            var parameters = new PageReplacementParameters()
            {
                MemorySize = 4
            };
            ReferenceRequests = string.Join("", parameters.ReferenceRequests);

            optimalSolution = new OptimalSolver().Solve(parameters).Steps;
            optimalSolution.RemoveAt(0);

            fifoSolution = new FIFOSolver().Solve(parameters).Steps;
            fifoSolution.RemoveAt(0);

            lruSolution = new LRUSolver().Solve(parameters).Steps;
            lruSolution.RemoveAt(0);

            clockSolution = new ClockSolver().Solve(parameters).Steps;
            clockSolution.RemoveAt(0);

            InitTableTest();
        }

        public Grid Table
        {
            get 
            { 
                return _Table; 
            }
            private set 
            {
                _Table = value; 
            }
        }

        private Grid _Table { get; set; }


        private void InitTableTest()
        {
            /*
            Neue Funktionen für TableGen selber programmieren oder Pascal überlassen --> Klare Trennung?


            
            -   Vorhande Funktionen auch für Zeilen und Spalten, nicht nur Zellen.
            -   (height, width) ist anders als bei Excel :( --> (width, height)
            -   Layout für Texte anpassen/anpassbar machen: Zentriert
            -   Title Row/Column? 

            */


            var tableGen = new TableGen.TableGen(10, 9, 25, 50);

            //tableGen.SetBorderForCell(0, 0, TableGen.Border.BorderPosition.Top);
            //tableGen.SetBorderForCell(0, 0, TableGen.Border.BorderPosition.Left);
            //tableGen.SetBorderForCell(0, 0, TableGen.Border.BorderPosition.Right);
            //tableGen.SetBorderForCell(0, 0, TableGen.Border.BorderPosition.Bot);

            //tableGen.SetBorderForCell(0, 1, TableGen.Border.BorderPosition.Top);
            //tableGen.SetBorderForCell(0, 1, TableGen.Border.BorderPosition.Left);
            //tableGen.SetBorderForCell(0, 1, TableGen.Border.BorderPosition.Right);
            //tableGen.SetBorderForCell(0, 1, TableGen.Border.BorderPosition.Bot);

            //tableGen.SetBorderForCell(1, 0, TableGen.Border.BorderPosition.Top);
            //tableGen.SetBorderForCell(1, 0, TableGen.Border.BorderPosition.Left);
            //tableGen.SetBorderForCell(1, 0, TableGen.Border.BorderPosition.Right);
            //tableGen.SetBorderForCell(1, 0, TableGen.Border.BorderPosition.Bot);

            //tableGen.SetBorderForCell(1, 1, TableGen.Border.BorderPosition.Top);
            //tableGen.SetBorderForCell(1, 1, TableGen.Border.BorderPosition.Left);
            //tableGen.SetBorderForCell(1, 1, TableGen.Border.BorderPosition.Right);
            //tableGen.SetBorderForCell(1, 1, TableGen.Border.BorderPosition.Bot);

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

            _Table = tableGen.Grid;
        }
    }
}