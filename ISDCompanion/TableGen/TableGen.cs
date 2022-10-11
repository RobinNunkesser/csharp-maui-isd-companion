using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls.Shapes;

namespace TableGen
{
    public class TableGen
    {
        private int _ColumnCount = 1;
        public int ColumnCount
        {
            get { return _ColumnCount; }

        }
        private int _RowCount = 1;
        public int RowCount
        {
            get { return _RowCount; }

        }
        private int _CellHeight = 1;
        public int CellHeight
        {
            get { return _CellHeight; }
            set
            {
                _CellHeight = value;
                for (int i = 0; i < RowCount; i++)
                {
                    SetRowHeight(i, value);
                }
            }
        }
        private int _CellWidth = 1;
        public int CellWidth
        {
            get { return _CellWidth; }
            set
            {
                _CellWidth = value;
                for (int i = 0; i < ColumnCount; i++)
                {
                    SetColumnWidth(i, value);
                }
            }
        }

        protected Grid _Grid = null;
        public Grid Grid
        {
            get { return _Grid; }
        }

        protected List<List<AbsoluteLayout>> _Cells = null;
        public List<List<AbsoluteLayout>> Cells
        {
            get
            {
                return _Cells;
            }
        }

        protected List<List<List<Border>>> _Borders = null;


        public TableGen(int ColumnCount, int RowCount, int CellHeight, int CellWidth)
        {
            this._ColumnCount = ColumnCount;
            this._RowCount = RowCount;
            this._CellHeight = CellHeight;
            this._CellWidth = CellWidth;
            _Borders = new List<List<List<Border>>>();
            _Cells = new List<List<AbsoluteLayout>>();
            Init();
        }
        public void AddElement(int row, int col, View view)
        {
            Cells[row][col].Children.Add(view);
        }

        public void AddElement(int row, int col, View view, int ColumnSpan, int RowSpan)
        {
            Cells[row][col].Children.Add(view);
            Grid.SetColumnSpan((BindableObject)Cells[row][col], ColumnSpan);
            Grid.SetRowSpan((BindableObject)Cells[row][col], RowSpan);
        }

        public void AddCenteredElement(int row, int col, View view)
        {
            view.Margin = new Thickness((CellWidth / 10) * 4, 0, 0, 0);
            Cells[row][col].Children.Add(view);
        }

        public void RemoveElements(int row, int col)
        {
            //Cells[row][col].Children.Clear();
            List<Label> labelsToRemove = new List<Label>();

            foreach (var child in Cells[row][col].Children)
            {
                if (child.GetType() == typeof(Label))
                {
                    labelsToRemove.Add((Label)child);
                }
            }

            foreach (Label label in labelsToRemove)
            {
                Cells[row][col].Children.Remove(label);
            }

        }

        public void ClearBorders(int row, int col)
        {
            foreach (var border in _Borders[row][col])
            {
                Cells[row][col].Children.Remove(border.Line);
            }
            _Borders[row][col].Clear();
        }

        public void RemoveBorder(int row, int col, Border.BorderPosition borderPosition)
        {
            var border = _Borders[row][col].FirstOrDefault(tmpBorder => tmpBorder.CurrentBorderPosition == borderPosition);
            if (border != null)
            {
                Cells[row][col].Children.Remove(border.Line);
                _Borders[row][col].Remove(border);
            }
        }

        public void SetBorderForRow(int row, Border.Stroke stroke = Border.Stroke.Solid)
        {
            for (int i = 0; i < ColumnCount; i++)
            {
                SetBorderForCell(row, i);
            }
        }

        public void SetBorderForColumn(int col, Border.Stroke stroke = Border.Stroke.Solid)
        {
            for (int i = 0; i < RowCount; i++)
            {
                SetBorderForCell(i, col);
            }
        }

        public void SetBorderForCell(int row, int col, Border.Stroke stroke = Border.Stroke.Solid)
        {
            SetBorderForCell(row, col, new Border.BorderPosition[4]
            {
                Border.BorderPosition.Top,
                Border.BorderPosition.Left,
                Border.BorderPosition.Right,
                Border.BorderPosition.Bot
            }, stroke);
        }

        public void SetBorderForCell(int row, int col, Border.BorderPosition borderPosition, Border.Stroke stroke = Border.Stroke.Solid)
        {
            SetBorderForCell(row, col, new Border.BorderPosition[1] { borderPosition }, stroke);
        }

        public void SetBorderForCell(int row, int col, Border.BorderPosition[] borderPositions, Border.Stroke stroke = Border.Stroke.Solid)
        {
            Brush brush = null;
            switch (stroke)
            {
                case Border.Stroke.Solid:
                    brush = new SolidColorBrush(Colors.Black);
                    //todo
                    break;

                case Border.Stroke.Dotted:
                    brush = new SolidColorBrush(Colors.Black);
                    //todo
                    break;
            }

            foreach (var borderPosition in borderPositions)
            {
                var border = _Borders[row][col].FirstOrDefault(tmpBorder => tmpBorder.CurrentBorderPosition == borderPosition);
                if (border == null)
                {
                    border = new Border()
                    {
                        CurrentBorderPosition = borderPosition,
                        Line = new Line()
                    };
                }

                switch (borderPosition)
                {
                    case Border.BorderPosition.Top:


                        border.Line.X1 = 0;
                        border.Line.X2 = Grid.ColumnDefinitions[col].Width.Value;
                        border.Line.Y1 = 0;
                        border.Line.Y2 = 0;
                        border.Line.Stroke = new SolidColorBrush(Colors.Black);

                        if (!_Borders[row][col].Contains(border))
                        {
                            _Borders[row][col].Add(border);
                            Cells[row][col].Children.Add(border.Line);
                        }
                        break;
                    case Border.BorderPosition.Left:

                        border.Line.X1 = 0;
                        border.Line.X2 = 0;
                        border.Line.Y1 = 0;
                        border.Line.Y2 = Grid.RowDefinitions[row].Height.Value;
                        border.Line.Stroke = new SolidColorBrush(Colors.Black);

                        if (!_Borders[row][col].Contains(border))
                        {
                            _Borders[row][col].Add(border);
                            Cells[row][col].Children.Add(border.Line);
                        }
                        break;
                    case Border.BorderPosition.Right:

                        border.Line.X1 = Grid.ColumnDefinitions[col].Width.Value;
                        border.Line.X2 = Grid.ColumnDefinitions[col].Width.Value;
                        border.Line.Y1 = 0;
                        border.Line.Y2 = Grid.RowDefinitions[row].Height.Value;
                        border.Line.Stroke = new SolidColorBrush(Colors.Black);

                        if (!_Borders[row][col].Contains(border))
                        {
                            _Borders[row][col].Add(border);
                            Cells[row][col].Children.Add(border.Line);
                        }
                        break;
                    case Border.BorderPosition.Bot:

                        border.Line.X1 = 0;
                        border.Line.X2 = Grid.ColumnDefinitions[col].Width.Value;
                        border.Line.Y1 = Grid.RowDefinitions[row].Height.Value;
                        border.Line.Y2 = Grid.RowDefinitions[row].Height.Value;
                        border.Line.Stroke = new SolidColorBrush(Colors.Black);

                        if (!_Borders[row][col].Contains(border))
                        {
                            _Borders[row][col].Add(border);
                            Cells[row][col].Children.Add(border.Line);
                        }
                        break;
                }
            }
        }

        protected void UpdateBorders(int row, int col)
        {
            if (row == -1 && col != -1)
            {
                for (int i = 0; i < RowCount; i++)
                {
                    row = i;
                    var borderTypes = new Border.BorderPosition[_Borders[row][col].Count];
                    int counter = 0;
                    foreach (var borderType in _Borders[row][col])
                    {
                        borderTypes[counter] = borderType.CurrentBorderPosition;
                        counter++;
                    }
                    SetBorderForCell(row, col, borderTypes, Border.Stroke.Solid);
                }
            }
            else if (col == -1 && row != -1)
            {
                for (int i = 0; i < ColumnCount; i++)
                {
                    col = i;
                    var borderTypes = new Border.BorderPosition[_Borders[row][col].Count];
                    int counter = 0;
                    foreach (var borderType in _Borders[row][col])
                    {
                        borderTypes[counter] = borderType.CurrentBorderPosition;
                        counter++;
                    }
                    SetBorderForCell(row, col, borderTypes, Border.Stroke.Solid);
                }
            }
            else
            {
                //todo unwichtig
            }

        }

        public void SetRowHeight(int row, int height)
        {
            Grid.RowDefinitions[row].Height = height;
            UpdateBorders(row, -1);
        }
        public void SetBackGroundColor(int Row, int Col, Color color)
        {
            Cells[Row][Col].BackgroundColor = color;
        }
        public void SetColumnWidth(int col, int width)
        {
            Grid.ColumnDefinitions[col].Width = width;
            UpdateBorders(-1, col);
        }

        protected void Init()
        {
            _Grid = new Grid()
            {
                RowSpacing = 0,
                ColumnSpacing = 0
            };
            //create Rows
            for (int i = 0; i < RowCount; i++)
            {
                _Grid.RowDefinitions.Add(new RowDefinition { Height = CellHeight });
            }
            //create Columns
            for (int i = 0; i < ColumnCount; i++)
            {
                _Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = CellWidth });
            }

            for (int i = 0; i < RowCount; i++)
            {
                _Cells.Add(new List<AbsoluteLayout>());
                _Borders.Add(new List<List<Border>>());
                for (int j = 0; j < ColumnCount; j++)
                {
                    var layout = new AbsoluteLayout()
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                    };

                    _Grid.Add(layout, j, i);
                    _Cells[i].Add(layout);
                    _Borders[i].Add(new List<Border>());
                }
            }
        }

    }
}
