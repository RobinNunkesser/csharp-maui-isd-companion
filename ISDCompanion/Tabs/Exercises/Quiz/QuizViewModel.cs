using System.Globalization;
using System.Windows.Input;
using Italbytz.Exam.Trivia.Abstractions;
using StudyCompanion.Resources.Strings;

namespace StudyCompanion
{
    public class QuizViewModel : ViewModel
    {
        private readonly Random _random = new();
        private readonly QuizCatalog[] _catalogs;
        private IQuestion[] _questions = Array.Empty<IQuestion>();
        private int _index;
        private int _correctAnswers;
        private int _wrongAnswers;
        private int _skippedQuestions;
        private int _currentStreak;
        private int _bestStreak;
        private int _selectedCatalogIndex;

        private sealed record QuizCatalog(string Title, Func<IQuestion[]> QuestionFactory);

        public int TotalQuestions => _questions.Length;

        public int AnsweredQuestions => CorrectAnswers + WrongAnswers + SkippedQuestions;

        public int CorrectAnswers
        {
            get => _correctAnswers;
            private set
            {
                if (_correctAnswers == value) return;
                _correctAnswers = value;
                OnPropertyChanged();
            }
        }

        public int WrongAnswers
        {
            get => _wrongAnswers;
            private set
            {
                if (_wrongAnswers == value) return;
                _wrongAnswers = value;
                OnPropertyChanged();
            }
        }

        public int SkippedQuestions
        {
            get => _skippedQuestions;
            private set
            {
                if (_skippedQuestions == value) return;
                _skippedQuestions = value;
                OnPropertyChanged();
            }
        }

        public int CurrentStreak
        {
            get => _currentStreak;
            private set
            {
                if (_currentStreak == value) return;
                _currentStreak = value;
                OnPropertyChanged();
            }
        }

        public int BestStreak
        {
            get => _bestStreak;
            private set
            {
                if (_bestStreak == value) return;
                _bestStreak = value;
                OnPropertyChanged();
            }
        }

        public IReadOnlyList<string> CatalogNames => _catalogs.Select(catalog => catalog.Title).ToArray();

        public int SelectedCatalogIndex
        {
            get => _selectedCatalogIndex;
            set
            {
                if (_selectedCatalogIndex == value) return;
                _selectedCatalogIndex = value;
                OnPropertyChanged();
                ResetQuiz();
            }
        }

        public string Question => HasActiveQuestion ? _questions[_index].Text : AppResources.QuizCompleted;

        public string CatalogSummaryText => string.Format(AppResources.QuizCatalogSummary, SelectedCatalogName, TotalQuestions);

        public string QuestionNumberText => HasActiveQuestion
            ? string.Format(AppResources.QuizQuestionNumber, _index + 1, TotalQuestions)
            : AppResources.title_statistics;

        public string SessionStatusText => HasActiveQuestion ? AppResources.QuizTrueFalseHint : AppResources.QuizCompleted;

        public string AccuracyText => AccuracyRatio.ToString("P0", CultureInfo.CurrentCulture);

        public string CompletionText => CompletionRatio.ToString("P0", CultureInfo.CurrentCulture);

        public int RemainingQuestions => Math.Max(0, TotalQuestions - AnsweredQuestions);

        public double AccuracyRatio => AnsweredQuestions == 0 ? 0 : (double)CorrectAnswers / AnsweredQuestions;

        public double CompletionRatio => TotalQuestions == 0 ? 0 : (double)AnsweredQuestions / TotalQuestions;

        public double CorrectRatio => AnsweredQuestions == 0 ? 0 : (double)CorrectAnswers / AnsweredQuestions;

        public double WrongRatio => AnsweredQuestions == 0 ? 0 : (double)WrongAnswers / AnsweredQuestions;

        public double SkippedRatio => AnsweredQuestions == 0 ? 0 : (double)SkippedQuestions / AnsweredQuestions;

        public bool HasActiveQuestion => _index < _questions.Length;

        public ICommand RestartQuizCommand { get; }

        private bool buttonsEnabled = true;
        public bool ButtonsEnabled
        {
            get => buttonsEnabled;
            set
            {
                if (value != buttonsEnabled)
                {
                    buttonsEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private string answer = string.Empty;
        public string Answer
        {
            get => answer;
            private set
            {
                if (value != answer)
                {
                    answer = value;
                    OnPropertyChanged();
                }
            }
        }

        private Color feedbackColor = Colors.Transparent;
        public Color FeedbackColor
        {
            get => feedbackColor;
            private set
            {
                if (value != feedbackColor)
                {
                    feedbackColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool HasFeedback => !string.IsNullOrWhiteSpace(Answer);

        public QuizViewModel()
        {
            _catalogs =
            [
                new QuizCatalog(AppResources.QuizAllCatalogs, () => Italbytz.Exam.Networking.YesNoQuestions.Questions.Concat(Italbytz.Exam.OperatingSystems.YesNoQuestions.Questions).Cast<IQuestion>().ToArray()),
                new QuizCatalog(AppResources.Networks, () => Italbytz.Exam.Networking.YesNoQuestions.Questions.Cast<IQuestion>().ToArray()),
                new QuizCatalog(AppResources.OperatingSystems, () => Italbytz.Exam.OperatingSystems.YesNoQuestions.Questions.Cast<IQuestion>().ToArray())
            ];

            RestartQuizCommand = new Command(ResetQuiz);
            ResetQuiz();
        }

        public void SubmitAnswer(bool value)
        {
            if (!HasActiveQuestion || _questions[_index] is not IYesNoQuestion question)
            {
                return;
            }

            var isCorrect = question.Answer == value;

            if (isCorrect)
            {
                CorrectAnswers++;
                CurrentStreak++;
                BestStreak = Math.Max(BestStreak, CurrentStreak);
                Answer = AppResources.Right;
                FeedbackColor = Color.FromArgb("#0F9D58");
            }
            else
            {
                WrongAnswers++;
                CurrentStreak = 0;
                Answer = string.Format(AppResources.QuizCorrectAnswer, question.Answer ? AppResources.Right : AppResources.Wrong);
                FeedbackColor = Color.FromArgb("#D93025");
            }

            RaiseSessionProperties();
        }

        public void SkipCurrentQuestion()
        {
            if (!HasActiveQuestion)
            {
                return;
            }

            SkippedQuestions++;
            CurrentStreak = 0;
            AdvanceToNextQuestion();
        }

        public void AdvanceToNextQuestion()
        {
            if (!HasActiveQuestion)
            {
                ButtonsEnabled = false;
                return;
            }

            _index++;
            Answer = string.Empty;
            FeedbackColor = Colors.Transparent;
            ButtonsEnabled = HasActiveQuestion;
            RaiseSessionProperties();
        }

        private void ResetQuiz()
        {
            _questions = _catalogs[_selectedCatalogIndex].QuestionFactory().ToArray();
            Shuffle(_questions);
            _index = 0;
            CorrectAnswers = 0;
            WrongAnswers = 0;
            SkippedQuestions = 0;
            CurrentStreak = 0;
            BestStreak = 0;
            Answer = string.Empty;
            FeedbackColor = Colors.Transparent;
            ButtonsEnabled = HasActiveQuestion;
            RaiseSessionProperties();
        }

        private string SelectedCatalogName => _catalogs[_selectedCatalogIndex].Title;

        private void Shuffle(IQuestion[] questions)
        {
            for (var index = questions.Length - 1; index > 0; index--)
            {
                var swapIndex = _random.Next(index + 1);
                (questions[index], questions[swapIndex]) = (questions[swapIndex], questions[index]);
            }
        }

        private void RaiseSessionProperties()
        {
            OnPropertyChanged(nameof(AnsweredQuestions));
            OnPropertyChanged(nameof(RemainingQuestions));
            OnPropertyChanged(nameof(Question));
            OnPropertyChanged(nameof(CatalogSummaryText));
            OnPropertyChanged(nameof(QuestionNumberText));
            OnPropertyChanged(nameof(SessionStatusText));
            OnPropertyChanged(nameof(AccuracyText));
            OnPropertyChanged(nameof(CompletionText));
            OnPropertyChanged(nameof(AccuracyRatio));
            OnPropertyChanged(nameof(CompletionRatio));
            OnPropertyChanged(nameof(CorrectRatio));
            OnPropertyChanged(nameof(WrongRatio));
            OnPropertyChanged(nameof(SkippedRatio));
            OnPropertyChanged(nameof(HasActiveQuestion));
            OnPropertyChanged(nameof(HasFeedback));
        }

    }
}

