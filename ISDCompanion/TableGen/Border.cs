using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Controls.Shapes;

namespace TableGen
{
    public class Border
    {
        public enum BorderPosition
        {
            Top = 0,
            Left = 1,
            Right = 2,
            Bot = 3
        }
        public enum Stroke
        {
            Solid = 0,
            Dotted = 1
        }


        public Border.BorderPosition CurrentBorderPosition = Border.BorderPosition.Top;
        public Line Line = null;
    }
}
