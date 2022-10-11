using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphGen.Classes
{
    public class Edge : GraphLine
    {
        public Node Source = null;
        public Node Target = null;

        public Edge(Node Source, Node Target, string Desc, GraphGen graphGen) : base(graphGen)
        {
            this.Source = Source;
            this.Target = Target;
            this.Desc = Desc;

            var values = new List<double[]>();

            var P1 = new double[] { Source.Position.CenterX, Source.Position.CenterY };
            var P2 = new double[] { Target.Position.CenterX, Target.Position.CenterY };

            var linearFunctions = new List<LinearFunction>();
            linearFunctions.Add(new LinearFunction(P1, P2, new double[] { GraphGen.MaxWidth, GraphGen.MaxWidth }));

            linearFunctions = GeneratePath(linearFunctions);

            foreach (var linearFunction in linearFunctions)
            {
                values.Add(new double[] { linearFunction.P1[0], linearFunction.P1[1] });
                values.Add(new double[] { linearFunction.P2[0], linearFunction.P2[1] });
            }

            var pos = CalculateLabelPositon(linearFunctions);

            Init(values, Desc, pos);
        }
        public double[] CalculateLabelPositon(List<LinearFunction> linearFunctions)
        {
            double length = 0;
            double[] pos = null;
            foreach (var linearFunction in linearFunctions)
            {
                length += linearFunction.GetLength();
            }

            length = length / 2;
            if (linearFunctions.Count > 1)
            {
                foreach (var linearFunction in linearFunctions)
                {
                    if (length >= linearFunction.GetLength())
                    {
                        length -= linearFunction.GetLength();
                    }
                    else
                    {
                        pos = linearFunction.GetPosForLength(length);
                        break;
                    }
                }
            }
            else
            {
                pos = linearFunctions[0].GetPosForLength(length / 1.5);
            }
            return pos;
        }

        public void Mark()
        {
            Polyline.StrokeThickness += 3;
        }
        public void UnMark()
        {
            Polyline.StrokeThickness -= 3;
        }

        protected bool CollisonFound(double x, double y)
        {
            var tmp = 0;
            if (GraphGen.IsInBounds(x, y, ref tmp) &&
                GraphGen.CollisonGrid[(int)y, (int)x] != Source.Counter &&
                GraphGen.CollisonGrid[(int)y, (int)x] != Target.Counter &&
                GraphGen.CollisonGrid[(int)y, (int)x] != 0)
            {
                return true;
            }
            return false;
        }


        protected List<LinearFunction> GeneratePath(List<LinearFunction> linearFunctions)
        {
            var tmp = 0;
            foreach (var linearFunction in linearFunctions)
            {
                if (linearFunction.GetYForX(0) == -999)
                {
                    //line is verticle => iterate y-achsis
                    //todo
                }
                else
                {
                    if (linearFunction.P1[0] < linearFunction.P2[0])
                    {
                        //iterate backwards
                        for (double x = linearFunction.P1[0]; x <= linearFunction.P2[0]; x += 0.05)
                        {
                            double y = linearFunction.GetYForX(x);

                            if (CollisonFound(x, y))
                            {
                                //collison found
                                var newpos = linearFunction.GetOnCollisonPosition(new double[] { x, y });
                                var newLinearFunction1 = new LinearFunction(linearFunction.P1, newpos, new double[] { GraphGen.MaxWidth, GraphGen.MaxHeight });
                                var newLinearFunction2 = new LinearFunction(newpos, linearFunction.P2, new double[] { GraphGen.MaxWidth, GraphGen.MaxHeight });

                                var newList = new List<LinearFunction>();
                                if (linearFunctions.Count > 1)
                                {
                                    foreach (var item in linearFunctions)
                                    {
                                        if (item == linearFunction)
                                        {
                                            newList.Add(newLinearFunction1);
                                            newList.Add(newLinearFunction2);
                                        }
                                        else
                                        {
                                            newList.Add(item);
                                        }
                                    }
                                }
                                else
                                {
                                    newList.Add(newLinearFunction1);
                                    newList.Add(newLinearFunction2);
                                }

                                //return GeneratePath(linearFunctions);
                                //

                                return GeneratePath(newList);
                            }

                        }
                    }
                    else
                    {
                        //iterate forwards
                        for (double x = linearFunction.P1[0]; x >= linearFunction.P2[0]; x -= 0.05)
                        {
                            double y = linearFunction.GetYForX(x);
                            if (CollisonFound(x, y))
                            {
                                //collison found
                                var newpos = linearFunction.GetOnCollisonPosition(new double[] { x, y });
                                var newLinearFunction1 = new LinearFunction(linearFunction.P1, newpos, new double[] { GraphGen.MaxWidth, GraphGen.MaxHeight });
                                var newLinearFunction2 = new LinearFunction(newpos, linearFunction.P2, new double[] { GraphGen.MaxWidth, GraphGen.MaxHeight });


                                var newList = new List<LinearFunction>();
                                if (linearFunctions.Count > 1)
                                {
                                    foreach (var item in linearFunctions)
                                    {
                                        if (item == linearFunction)
                                        {
                                            newList.Add(newLinearFunction1);
                                            newList.Add(newLinearFunction2);
                                        }
                                        else
                                        {
                                            newList.Add(item);
                                        }
                                    }
                                }
                                else
                                {
                                    newList.Add(newLinearFunction1);
                                    newList.Add(newLinearFunction2);
                                }


                                //return linearFunctions;


                                return GeneratePath(newList);
                            }
                        }
                    }
                }
            }

            return linearFunctions;
        }


    }
}
