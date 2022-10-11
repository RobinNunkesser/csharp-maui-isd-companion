using System;
using System.Collections.Generic;
using System.Text;

namespace GraphGen.Classes
{
    public class SimpleEdge : Edge
    {
        private SimpleNode SimpleSource { get; set; }
        private SimpleNode SimpleTarget { get; set; }

        private SimpleGraphGen SimpleGraphGen { get; set; }

        private int _distanceFactor = 25;

        public SimpleEdge(SimpleNode Source, SimpleNode Target, string Desc, SimpleGraphGen graphGen) : base(Source, Target, Desc, graphGen)
        {

        }
        public double[] CalculateLabelPositon(List<double[]> values)
        {
            if (!(values.Count % 2 == 0))
            {
                throw new Exception("uneven");
            }
            var linearFunctions = new List<LinearFunction>();
            for (int i = 0; i < values.Count / 2; i++)
            {
                double[] p1 = new double[2]
                {
                    values[i][0],
                    values[i][1]
                };
                double[] p2 = new double[2]
{
                    values[i+1][0],
                    values[i+1][1]
};

                linearFunctions.Add(new LinearFunction(p1, p2, new double[] { GraphGen.MaxWidth, GraphGen.MaxHeight }));
            }

            return CalculateLabelPositon(linearFunctions);
        }
        public override void Init(List<double[]> values, string Desc, double[] lablePos)
        {
            SimpleSource = (SimpleNode)Source;
            SimpleTarget = (SimpleNode)Target;
            SimpleGraphGen = (SimpleGraphGen)GraphGen;

            //check here if we have a prefab for the edge

            //If they are neighbors
            if (Math.Abs(SimpleSource.Position.InternalPosition[0] - SimpleTarget.Position.InternalPosition[0]) == 1)
            {
                //base pathfinding
                base.Init(values, Desc, lablePos);

            }
            //if they are on the same x level but not neighbors 
            else if (SimpleSource.Position.InternalPosition[1] == SimpleTarget.Position.InternalPosition[1] &&
                Math.Abs(SimpleSource.Position.InternalPosition[0] - SimpleTarget.Position.InternalPosition[0]) > 1)
            {

                double nodeDistance = Math.Abs(SimpleSource.Position.InternalPosition[0] - SimpleTarget.Position.InternalPosition[0]);
                //top row
                if (SimpleSource.Position.InternalPosition[1] == 0)
                {
                    double SourceOffset = SimpleSource.GetHorizontalBranchOffset();
                    double TargetOffset = SimpleTarget.GetHorizontalBranchOffset();

                    List<double[]> newValues = new List<double[]>();
                    newValues.Add(new double[2]);
                    //init pos
                    newValues[0][0] = SimpleSource.Position.CenterX + SourceOffset;
                    newValues[0][1] = SimpleSource.Position.CenterY;

                    newValues.Add(new double[2]);
                    //moving up
                    newValues[1][0] = SimpleSource.Position.CenterX + SourceOffset;
                    newValues[1][1] = SimpleSource.Position.CenterY - (_distanceFactor * nodeDistance);
                    newValues.Add(new double[2]);
                    //moving right/left
                    newValues[2][0] = SimpleTarget.Position.CenterX + TargetOffset;
                    newValues[2][1] = newValues[1][1];
                    newValues.Add(new double[2]);
                    //moving down
                    newValues[3][0] = SimpleTarget.Position.CenterX + TargetOffset;
                    newValues[3][1] = SimpleTarget.Position.CenterY;
                    base.Init(newValues, Desc, CalculateLabelPositon(newValues));
                }
                //last row
                else if (SimpleSource.Position.InternalPosition[1] == SimpleGraphGen.MaxRowCount())
                {
                    double SourceOffset = SimpleSource.GetHorizontalBranchOffset();
                    double TargetOffset = SimpleTarget.GetHorizontalBranchOffset();

                    List<double[]> newValues = new List<double[]>();
                    newValues.Add(new double[2]);
                    //init pos
                    newValues[0][0] = SimpleSource.Position.CenterX + SourceOffset;
                    newValues[0][1] = SimpleSource.Position.CenterY;
                    newValues.Add(new double[2]);
                    //moving up
                    newValues[1][0] = SimpleSource.Position.CenterX + SourceOffset;
                    newValues[1][1] = SimpleSource.Position.CenterY + (_distanceFactor * nodeDistance);
                    newValues.Add(new double[2]);
                    //moving right/left
                    newValues[2][0] = SimpleTarget.Position.CenterX + TargetOffset;
                    newValues[2][1] = newValues[1][1];
                    newValues.Add(new double[2]);
                    //moving down
                    newValues[3][0] = SimpleTarget.Position.CenterX + TargetOffset;
                    newValues[3][1] = SimpleTarget.Position.CenterY;

                    base.Init(newValues, Desc, CalculateLabelPositon(newValues));
                }
                //any other row
                else
                {
                    //base pathfinding
                    base.Init(values, Desc, lablePos);
                }
            }
            else
            {
                //base pathfinding
                base.Init(values, Desc, lablePos);
            }
        }
    }
}
