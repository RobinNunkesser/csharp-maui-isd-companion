using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphGen.Classes
{
    public class GraphGen
    {
        private int _CurrentColor = 0;
        private Color[] UsedColors = new Color[]
        {
            Colors.Black,
            Colors.Blue,
            Colors.Green,
            Colors.Red,
            Colors.Purple,
            Colors.Orange,
        };
        public Color GetNewColor()
        {
            _CurrentColor++;
            if (_CurrentColor > UsedColors.Length - 1)
            {
                _CurrentColor = 0;

            }
            return UsedColors[_CurrentColor];
        }


        public List<Node> Nodes { get; set; }
        protected List<double> UsedDeg = new List<double>();
        protected List<double> ExceptDeg = new List<double>() { -90, -180, -270 };

        protected int OutOfBoundsCounter = 0;

        public static Position InitialPosition = new Position
        {
            X = 5,
            Y = 100,
        };

        protected Position CurrentPosition = new Position();
        protected int Layer = 0;


        protected double CurrentDegStep = 45;

        protected double CurrentDeg = -90;

        public int MaxWidth = 0;
        public int MaxHeight = 0;

        //row,column => y,x
        public int[,] CollisonGrid = null;
        protected bool _IsFull = false;
        public bool IsFull { get { return _IsFull; } }

        public GraphGen(int MaxWidth, int MaxHeight)
        {
            this.MaxWidth = MaxWidth;
            this.MaxHeight = MaxHeight;
            CollisonGrid = new int[MaxHeight, MaxWidth];

            Nodes = new List<Node>();
        }

        public virtual void AddNewNode(string NodeName)
        {
            if (!IsFull)
            {
                try
                {
                    Node node = new Node
                    (
                        NodeName,
                        this
                    );
                    var FailureCounter = 0;
                    var pos = GetNewPositon(ref FailureCounter);
                    node.Position = pos;
                    Nodes.Add(node);
                    node.Counter = Nodes.Count;
                    for (int y = 0; y < 30; y++)
                    {
                        for (int x = 0; x < 30; x++)
                        {
                            CollisonGrid[(int)pos.Y + y, (int)pos.X + x] = Nodes.Count;

                        }
                    }

                }
                catch (Exception ex)
                {
                    if (ex.Message != "out of bound")
                    {
                        throw ex;
                    }
                    else
                    {
                        _IsFull = true;
                    }
                }
            }
        }

        protected virtual Position GetNewPositon(ref int FailureCounter)
        {
            if (FailureCounter > 20)
            {
                throw new Exception("out of bound");
            }

            if (GetNodes().Count != 0)
            {
                var Pos = new Position();
                var oldDeg = CurrentDeg;

                Pos.X = ((Layer * Node.Radius * 5 * Math.Cos((Math.PI * CurrentDeg) / 180)) - (InitialPosition.X)) * -1;
                Pos.Y = ((Layer * Node.Radius * 5 * Math.Sin((Math.PI * CurrentDeg) / 180)) - (InitialPosition.Y)) * -1;


                if (CurrentDeg <= -270)
                {
                    Layer++;
                    CurrentDeg = -90;
                    CurrentDegStep /= 2;
                }


                CurrentDeg -= CurrentDegStep;

                if (!ExceptDeg.Contains(oldDeg) && UsedDeg.Contains(oldDeg))
                {
                    CurrentPosition = GetNewPositon(ref FailureCounter);
                }
                else if (!IsInBounds(Pos.X, Pos.Y, ref FailureCounter))
                {
                    CurrentPosition = GetNewPositon(ref FailureCounter);
                }
                else
                {
                    UsedDeg.Add(oldDeg);
                    CurrentPosition = Pos;
                }

            }
            else
            {
                //Start Position
                Layer = 1;
                CurrentPosition = InitialPosition;
            }
            FailureCounter = 0;
            return CurrentPosition;

        }
        public virtual Node GetNode(string NodeName)
        {
            return GetNodes().FirstOrDefault(x => x.Name == NodeName);
        }

        public bool IsInBounds(double x, double y, ref int FailureCounter)
        {
            if (x <= 30 || y <= 30 || x >= MaxWidth - 30 || y >= MaxHeight - 30)
            {
                FailureCounter++;
                return false;
            }
            return true;
        }
        protected virtual List<Node> GetNodes()
        {
            return Nodes;
        }

        public AbsoluteLayout RenderLayout()
        {
            var layout = new AbsoluteLayout() { VerticalOptions = LayoutOptions.FillAndExpand };

            foreach (var node in GetNodes())
            {

                foreach (var edge in node.Edges)
                {

                    layout.Children.Add(edge.View);
                }

            }

            foreach (var node in GetNodes())
            {
                AbsoluteLayout.SetLayoutBounds(node.View, new Rect(
                    node.Position.X,
                    node.Position.Y,
                    Node.Radius,
                    Node.Radius));
                layout.Children.Add(node.View);

            }

            return layout;
        }


    }
}
