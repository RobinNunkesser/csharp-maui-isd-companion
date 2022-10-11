using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Shapes;

namespace GraphGen.Classes
{
    public class Node : GraphEllipse
    {
        public static int Radius = 30;

        public int Counter { get; set; }

        public List<Edge> Edges { get; set; }

        public string Name
        {
            get
            {
                if (_Label == null)
                {
                    return "";
                }

                return _Label.Text;
            }
            set
            {
                if (_Label != null)
                {
                    _Label.Text = value;
                }
            }
        }

        protected Label _Label = null;


        public Node(int[] values, GraphGen graphGen) : base(values, graphGen) => Init();
        public Node(string Name, GraphGen graphGen) : base(new int[2]
        {
            Radius*2, //width
            Radius*2  //height
        }, graphGen)
        {
            _Label = new Label() { Text = Name };
            this.Name = Name;

            AbsoluteLayout.SetLayoutBounds(_Label, new Rect(10, 8, Radius, Radius));
            this.View.Children.Add(_Label);
            Init();
        }

        protected virtual void Init() => Edges = new List<Edge>();

        public Edge GetEdgeTo(Node target)
        {
            return Edges.FirstOrDefault((edge) => edge.Target == target);
        }

        public virtual void AddEdgeTo(Node target, string Desc)
        {
            if (target == null)
            {
                return;
            }

            Edges?.Add(new Edge
            (
                 this,
                 target,
                 Desc,
                 GraphGen
            ));
        }
    }
}
