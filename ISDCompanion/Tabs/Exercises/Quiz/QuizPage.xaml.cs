﻿using Italbytz.Ports.Trivia;

namespace StudyCompanion
{
    public partial class QuizPage : ContentPage
    {
        public QuizPage(IQuestion[] questions)
        {
            InitializeComponent();
            BindingContext = new QuizViewModel(questions);
        }

        async void Answer_Clicked(object sender, System.EventArgs e)
        {
            falseButton.IsEnabled = false;
            trueButton.IsEnabled = false;
            skipButton.IsEnabled = false;
            await answerLabel.FadeTo(1, 1000);
            await answerLabel.FadeTo(0, 200);
            falseButton.IsEnabled = true;
            trueButton.IsEnabled = true;
            skipButton.IsEnabled = true;
        }
    }
}
