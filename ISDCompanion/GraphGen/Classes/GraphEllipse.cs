using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Shapes;

namespace GraphGen.Classes
{
    public class GraphEllipse : Drawable
    {
        public GraphEllipse(int[] values, GraphGen graphGen) : base(graphGen)
        {
            var view = new AbsoluteLayout() { VerticalOptions = LayoutOptions.FillAndExpand };


            var ellipse = new Ellipse()
            {
                Stroke = new SolidColorBrush(Colors.Black),
                WidthRequest = values[0] / 2,
                BackgroundColor = Colors.Transparent,
                HeightRequest = values[1] / 2,

                HorizontalOptions = LayoutOptions.Center,
            };
            ellipse.Fill = new SolidColorBrush(Colors.White);
            //AbsoluteLayout.SetLayoutBounds(ellipse, new Xamarin.Forms.Rectangle(0, 0, values[0]/2 , values[0]/2 ));

            view.Children.Add(ellipse);

            _View = view;
        }
    }
}
