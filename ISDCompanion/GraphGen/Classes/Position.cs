using System;
using System.Collections.Generic;
using System.Text;

namespace GraphGen.Classes
{
    public class Position
    {
        public double X { get; set; }
        public double Y { get; set; }

        public double[] InternalPosition = new double[2];

        public double CenterX
        {
            get
            {
                return X + Node.Radius / 2;
            }
        }
        public double CenterY
        {
            get
            {
                return Y + Node.Radius / 2;
            }
        }
        public double[] GetAsArray()
        {
            return new double[]
            {
                CenterX, CenterY
            };
        }

    }
}
