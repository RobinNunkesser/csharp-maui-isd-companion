using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GraphGen.Classes
{
    public class SimpleGraphGen : GraphGen
    {

        protected int _CurrentPosition = 0;
        protected Position[] positions = null;
        public new List<SimpleNode> Nodes { get; set; }
        public Position[] Positions
        {
            get
            {
                return positions;
            }
        }


        private static int _distanceBetweenNodes = 100;
        public static int DistanceFactor
        {
            get
            {
                return _distanceBetweenNodes;
            }
        }

        public static int CalcMaxWidth(int nodeCount)
        {
            return Convert.ToInt32((InitialPosition.X * 2) + (nodeCount * _distanceBetweenNodes));
        }
        public static int CalcMaxHeight()
        {
            var tmp = Convert.ToInt32(((1.5 * _distanceBetweenNodes) + InitialPosition.Y * 2));
            return tmp;
        }

        public SimpleGraphGen(int nodeCount) : base(CalcMaxWidth(nodeCount), CalcMaxHeight())
        {
            Nodes = new List<SimpleNode>();
            positions = new Position[nodeCount];

            int firstRowCount = 0;
            int secondRowCount = 0;
            if (nodeCount % 2 == 0)
            {
                firstRowCount = nodeCount / 2;
                secondRowCount = nodeCount / 2;
            }
            else
            {
                firstRowCount = (nodeCount - 1) / 2;
                secondRowCount = (nodeCount + 1) / 2;
            }
            for (int i = 0; i < nodeCount; i++)
            {
                positions[i] = new Position();


                if (i < firstRowCount)
                {
                    positions[i].X = InitialPosition.X + (i * _distanceBetweenNodes);
                    positions[i].InternalPosition[0] = i;

                    positions[i].Y = InitialPosition.Y;
                    positions[i].InternalPosition[1] = 0;
                }
                else
                {
                    positions[i].X = InitialPosition.X + ((i - firstRowCount) * _distanceBetweenNodes);
                    positions[i].InternalPosition[0] = i - firstRowCount;

                    positions[i].Y = InitialPosition.Y + (1.5 * _distanceBetweenNodes);
                    positions[i].InternalPosition[1] = 1;
                }

            }
        }

        public double MaxRowCount()
        {
            var tmp = Nodes.OrderBy(o => o.Position.InternalPosition[1]).ToList();
            return tmp[tmp.Count - 1].Position.InternalPosition[1];
        }
        public double MaxColCount()
        {
            var tmp = Nodes.OrderBy(o => o.Position.InternalPosition[0]).ToList();
            return tmp[tmp.Count - 1].Position.InternalPosition[0];
        }

        public override void AddNewNode(string NodeName)
        {
            SimpleNode node = new SimpleNode(NodeName, this);
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
        public new SimpleNode GetNode(string NodeName)
        {
            return Nodes.FirstOrDefault(x => x.Name == NodeName);
        }
        protected override List<Node> GetNodes()
        {
            return Nodes.Cast<Node>().ToList();
        }

        protected override Position GetNewPositon(ref int FailureCounter)
        {
            try
            {
                if (_CurrentPosition > positions.Length - 1)
                {
                    _CurrentPosition = 0;

                }
                return positions[_CurrentPosition];
            }
            finally
            {
                _CurrentPosition++;
            }
        }

    }
}
