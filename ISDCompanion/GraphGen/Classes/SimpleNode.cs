using System;
using System.Collections.Generic;
using System.Text;

namespace GraphGen.Classes
{
    public class SimpleNode : Node
    {
        private new SimpleGraphGen GraphGen = null;

        public SimpleNode(string Name, SimpleGraphGen graphGen) : base(Name, graphGen)
        {
            GraphGen = graphGen;
        }

        public void AddEdgeTo(SimpleNode target, string Desc)
        {
            if (target == null)
            {
                return;
            }

            Edges?.Add(new SimpleEdge
            (
                 this,
                 target,
                 Desc,
                 GraphGen
            ));
        }
        public int GetMaxHorizontalBranches()
        {
            if (GraphGen.MaxColCount() == 2)
            {
                return 0;
            }
            //outside nodes
            else if (Position.InternalPosition[0] == 0 && Position.InternalPosition[0] == GraphGen.MaxColCount())
            {
                return (int)GraphGen.MaxColCount() - 1;
            }
            else
            {
                return (int)GraphGen.MaxColCount() - 2;
            }
        }
        private int _HorizontalBranchOffsetCounter = 0;
        private bool _flipOffset = false;
        public double GetHorizontalBranchOffset()
        {
            if (GetMaxHorizontalBranches() == 0)
            {
                return 0;
            }
            else
            {
                int flipFactor = 1;
                if (_flipOffset)
                {
                    flipFactor = -1;
                    _HorizontalBranchOffsetCounter++;
                }

                var offset = ((Node.Radius / 2) / GetMaxHorizontalBranches()) * _HorizontalBranchOffsetCounter * flipFactor;
                _flipOffset = !_flipOffset;

                return offset;
            }
        }

    }
}
