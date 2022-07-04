using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ISDCompanion.Exercises
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExerciseContentView : ContentView
    {
        public ExerciseContentView()
        {
            InitializeComponent();
        }
        public void ScrollToPosition(int x, int y, bool isAnimated)
        {
            var animation = new Animation(
                callback: x => scrollView.ScrollToAsync(x, y, animated: false),
                start: scrollView.ScrollX,
                end: x);

            animation.Commit(
                owner: this,
                name: "Scroll",
                length: 300,
                easing: Easing.SinInOut);
        }
    }
}