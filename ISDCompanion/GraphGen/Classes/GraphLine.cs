using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Shapes;

namespace GraphGen.Classes
{
    public class GraphLine : Drawable
    {

        protected Color Color { get; set; }

        protected Label _label = null;
        public Polyline Polyline = null;

        public string Desc
        {
            get
            {
                if (_label != null)
                {
                    return _label.Text;
                }
                else
                {
                    return "";
                }

            }
            set
            {
                if (_label != null)
                {
                    _label.Text = value;
                }

            }
        }

        public List<double[]> Pos = new List<double[]>();

        public GraphLine(GraphGen graphGen) : base(graphGen)
        {
            Color = graphGen.GetNewColor();
        }


        public virtual void Init(List<double[]> values, string Desc, double[] lablePos)
        {
            _View = new AbsoluteLayout();
            Pos = values;
            var tempPointCollection = new PointCollection();
            foreach (var value in values)
            {
                tempPointCollection.Add(new Point(value[0], value[1]));
            }

            Polyline = new Polyline()
            {
                Points = tempPointCollection,
                Stroke = new SolidColorBrush(Color),
            };
            _label = new Label();
            AbsoluteLayout.SetLayoutBounds(_label, new Rect(lablePos[0] + 2, lablePos[1] + 2, Desc.Length * 10, 15));
            _label.Text = Desc;
            _label.TextColor = Color;
            _label.BackgroundColor = Colors.Transparent;
            _label.VerticalTextAlignment = TextAlignment.Center;

            _View.Children.Add(Polyline);
            _View.Children.Add(_label);
        }

    }
}
