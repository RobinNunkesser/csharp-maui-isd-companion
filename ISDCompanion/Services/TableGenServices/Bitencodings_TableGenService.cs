using ISDCompanion.Resources.Strings;
using Italbytz.Adapters.Exam.Networks;
using Italbytz.Ports.Exam.Networks;

namespace ISDCompanion
{
    internal class Bitencodings_TableGenService : ITableGenService
    {
        private TableGen.TableGen tableGen;

        private int _index;
        private int _cellWidth = 25;
        BitencodingParameters _parameters;
        IBitencodingSolution _solution;

        private int currentColumnOfInterest { get; set; }

        public int X_CoordoninatesOfInterest()
        {
            return (currentColumnOfInterest - 3) * _cellWidth;
        }

        public int Y_CoordoninatesOfInterest()
        {
            return 0;
        }

        Color Color_Transparent = Colors.Transparent;

        public Bitencodings_TableGenService(BitencodingParameters parameters, IBitencodingSolution solution)
        {
            int length = parameters.Bits.Length;
            tableGen = new TableGen.TableGen(length, 5, 25, 25);
            _index = 0;
            currentColumnOfInterest = 0;
            _parameters = parameters;
            _solution = solution;
        }

        public Grid GenerateTable_TableHeader()
        {
            TableGen.TableGen tableGen_TableHeader = new TableGen.TableGen(1, 5, 25, 80);

            for (int i = 0; i < 4; i++)
            {
                tableGen_TableHeader.SetBackGroundColor(i, 0, Colors.Transparent);
            }

            List<Label> labels = new List<Label>();

            labels.Add(new Label() { Text = "Bits" });
            labels.Add(new Label() { Text = "NRZ" });
            labels.Add(new Label() { Text = "NRZ-I" });
            labels.Add(new Label() { Text = "MLT-3" });

            tableGen_TableHeader.AddElement(0, 0, labels[0]);
            tableGen_TableHeader.AddElement(2, 0, labels[1]);
            tableGen_TableHeader.AddElement(3, 0, labels[2]);
            tableGen_TableHeader.AddElement(4, 0, labels[3]);

            return tableGen_TableHeader.Grid;
        }

        public Grid GenerateTable_EmptyTable()
        {
            tableGen.SetBorderForRow(0);
            tableGen.SetBorderForRow(2);
            tableGen.SetBorderForRow(3);
            tableGen.SetBorderForRow(4);

            for (int i = 0; i < _parameters.Bits.Length; i++)
            {
                tableGen.AddCenteredElement(0, i, new Label() { Text = _parameters.Bits[i].ToString() });
            }

            return tableGen.Grid;
        }

        public Grid GenerateTable_NextStep()
        {
            if (_index < _parameters.Bits.Length)
            {
                string[] solutions = { _solution.NRZ[_index], _solution.NRZI[_index], _solution.MLT3[_index] };

                for (int j = 0; j < 3; j++)
                {
                    tableGen.AddCenteredElement(j + 2, _index, new Label() { Text = solutions[j] });
                }
                _index++;
                currentColumnOfInterest = _index;
            }

            return tableGen.Grid;
        }

        public Grid GenerateTable_PreviousStep()
        {
            if (_index > 0)
            {
                _index--;
                for (int j = 0; j < 3; j++)
                {
                    tableGen.RemoveElements(j + 2, _index);
                }
            }

            return tableGen.Grid;
        }

        public Grid GenerateTable_ShowSolution()
        {
            while (_index < _parameters.Bits.Length)
            {
                GenerateTable_NextStep();
            }

            return tableGen.Grid;
        }

        public String GetInfoText()
        {
            return AppResources.InfoText_BitEncoding;
        }

        public bool InfoAvailable()
        {
            return true;
        }
    }
}