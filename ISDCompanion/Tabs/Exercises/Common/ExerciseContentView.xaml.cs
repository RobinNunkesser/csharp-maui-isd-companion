namespace StudyCompanion;

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

    public void SetHeaderHeight(double value)
    {
        ContentGrid.RowDefinitions[0].Height = new GridLength(value);
    }
}
