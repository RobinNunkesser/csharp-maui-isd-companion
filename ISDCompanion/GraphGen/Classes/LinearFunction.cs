using System;
using System.Collections.Generic;
using System.Text;

namespace GraphGen.Classes
{
    public class LinearFunction
    {
        public double[] P1 = null;
        public double[] P2 = null;

        protected double B = -999;
        protected double M = -999;

        protected double[] Bounds = null;

        public LinearFunction(double[] P1, double[] P2, double[] bounds)
        {
            this.P1 = P1;
            this.P2 = P2;
            Bounds = bounds;
            CalcFunctionParameters();
        }
        protected void CalcFunctionParameters()
        {
            var tmp = new decimal[2]
            {
                (decimal)P2[1] - (decimal)P1[1],
                (decimal)P2[0] - (decimal)P1[0],
            };
            if (tmp[1] == 0)
            {
                return;
            }
            M = (double)tmp[0] / (double)tmp[1];
            B = P1[1] + (M * P1[0] * -1);

        }

        public double GetYForX(double X)
        {
            if (M == -999 || B == -999)
            {
                return -1;
            }
            return M * X + B;
        }
        public double[] GetOnCollisonPosition(double[] pos)
        {

            if (M == -999)
            {
                return null;
            }
            //x,y
            var ret = new double[2];

            var angleInDeg = ConvertRadiansToDegrees(Math.Atan(1 / this.M));

            ret[0] = (pos[0] + 80 * Math.Cos(angleInDeg));
            ret[1] = (pos[1] + 80 * Math.Sin(angleInDeg));

            if (ret[0] > Bounds[0])
            {
                ret[0] = Bounds[0];
            }
            if (ret[1] > Bounds[1])
            {
                ret[1] = Bounds[1];
            }
            return ret;
        }

        public double GetAngle()
        {

            if (M == -999)
            {
                return -999;
            }
            return ConvertRadiansToDegrees(Math.Atan(1 / this.M));
        }

        public double GetLength()
        {
            return Math.Sqrt(Math.Pow((P2[0] - P1[0]), 2) + Math.Pow((P2[1] - P1[1]), 2));
        }

        public double[] GetPosForLength(double length)
        {
            if (M == -999)
            {
                return new double[2]
                {
                    P1[0],
                    P1[1]+length
                };
            }


            var ret = new double[2];


            ret[0] = P1[0] - (length * (P1[0] - P2[0]) / GetLength());
            ret[1] = GetYForX(ret[0]);

            return ret;
        }



        public double ConvertRadiansToDegrees(double radians)
        {
            double degrees = (180 / Math.PI) * radians;
            return (degrees);
        }
    }
}
