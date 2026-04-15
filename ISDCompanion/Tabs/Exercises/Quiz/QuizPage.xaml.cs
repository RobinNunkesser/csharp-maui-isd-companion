namespace StudyCompanion;

public partial class QuizPage : ContentPage
{
    readonly QuizViewModel _viewModel;

    public QuizPage() : this(new QuizViewModel())
    {
    }

    public QuizPage(QuizViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    async void Wrong_Clicked(object sender, System.EventArgs e)
    {
        await HandleAnswerAsync(false);
    }

    async void Right_Clicked(object sender, System.EventArgs e)
    {
        await HandleAnswerAsync(true);
    }

    void Skip_Clicked(object sender, EventArgs e)
    {
        if (!_viewModel.ButtonsEnabled || !_viewModel.HasActiveQuestion)
        {
            return;
        }

        _viewModel.ButtonsEnabled = false;
        _viewModel.SkipCurrentQuestion();
    }

    async void Statistics_Clicked(object sender, System.EventArgs e)
    {
        await Navigation.PushAsync(new QuizStatisticsPage(_viewModel));
    }

    private async Task HandleAnswerAsync(bool value)
    {
        if (!_viewModel.ButtonsEnabled || !_viewModel.HasActiveQuestion)
        {
            return;
        }

        _viewModel.ButtonsEnabled = false;
        _viewModel.SubmitAnswer(value);
        answerLabel.Opacity = 0;
        await answerLabel.FadeToAsync(1, 180);
        await Task.Delay(900);
        await answerLabel.FadeToAsync(0, 150);
        _viewModel.AdvanceToNextQuestion();
    }
}
