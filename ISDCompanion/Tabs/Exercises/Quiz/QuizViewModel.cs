using System.Globalization;
using System.Text.Json;
using System.Windows.Input;
using Italbytz.Trivia.OpenTriviaDb;
using Italbytz.Trivia.Abstractions;
using StudyCompanion.Resources.Strings;

namespace StudyCompanion
{
    public class QuizViewModel : ViewModel
    {
        private const string QuizHistoryPreferenceKey = "QuizHistory";
        private const string QuizCachePreferenceKey = "QuizCatalogCache";
        private const string QuizOpenTriviaSessionTokenPreferenceKey = "QuizOpenTriviaSessionToken";
        private readonly Random _random = new();
        private readonly QuizCatalog[] _catalogs;
        private readonly OpenTriviaDbClient _openTriviaClient = new();
        private QuizHistoryStore _historyStore = QuizHistoryStore.CreateDefault();
        private QuizCacheStore _cacheStore = QuizCacheStore.CreateDefault();
        private IReadOnlyList<QuizAnswerOption> _answerOptions = Array.Empty<QuizAnswerOption>();
        private IQuestion[] _questions = Array.Empty<IQuestion>();
        private int _index;
        private int _correctAnswers;
        private int _wrongAnswers;
        private int _skippedQuestions;
        private int _currentStreak;
        private int _bestStreak;
        private int _selectedCatalogIndex;
        private int _catalogLoadVersion;
        private bool _buttonsEnabled;
        private bool _isLoading;
        private bool _isUsingCachedQuestions;
        private string _answer = string.Empty;
        private string _loadErrorMessage = string.Empty;
        private string _openTriviaSessionToken = string.Empty;
        private Color _feedbackColor = Colors.Transparent;

        private sealed record QuizCatalog(string Id, string Title, Func<CancellationToken, Task<QuizCatalogLoadResult>> LoadQuestionsAsync);

        private sealed record QuizCatalogLoadResult(IQuestion[] Questions, bool FromCache = false);

        private sealed class QuizHistoryStore
        {
            public QuizHistoryEntry Overall { get; set; } = new();
            public Dictionary<string, QuizHistoryEntry> Catalogs { get; set; } = new(StringComparer.Ordinal);

            public static QuizHistoryStore CreateDefault()
            {
                return new QuizHistoryStore
                {
                    Catalogs = new Dictionary<string, QuizHistoryEntry>(StringComparer.Ordinal)
                };
            }
        }

        private sealed class QuizHistoryEntry
        {
            public int SessionsPlayed { get; set; }
            public int QuestionsAnswered { get; set; }
            public int CorrectAnswers { get; set; }
            public int WrongAnswers { get; set; }
            public int SkippedQuestions { get; set; }
            public int BestStreak { get; set; }
            public string LastPlayedUtc { get; set; } = string.Empty;
        }

        private sealed class QuizCacheStore
        {
            public Dictionary<string, QuizCacheEntry> Catalogs { get; set; } = new(StringComparer.Ordinal);

            public static QuizCacheStore CreateDefault()
            {
                return new QuizCacheStore
                {
                    Catalogs = new Dictionary<string, QuizCacheEntry>(StringComparer.Ordinal)
                };
            }
        }

        private sealed class QuizCacheEntry
        {
            public string RawResponse { get; set; } = string.Empty;
            public string CachedAtUtc { get; set; } = string.Empty;
        }

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

        public IReadOnlyList<QuizAnswerOption> AnswerOptions => _answerOptions;

        public int SelectedCatalogIndex
        {
            get => _selectedCatalogIndex;
            set
            {
                if (_selectedCatalogIndex == value) return;
                _selectedCatalogIndex = value;
                OnPropertyChanged();
                _ = ResetQuizAsync();
            }
        }

        public string Question
        {
            get
            {
                if (IsLoading)
                {
                    return GetLocalizedString("QuizLoading", "Loading questions...");
                }

                if (HasLoadError)
                {
                    return LoadErrorMessage;
                }

                return HasActiveQuestion ? _questions[_index].Text : AppResources.QuizCompleted;
            }
        }

        public string CatalogSummaryText => string.Format(AppResources.QuizCatalogSummary, SelectedCatalogName, TotalQuestions);

        public string QuestionNumberText => HasActiveQuestion
            ? string.Format(AppResources.QuizQuestionNumber, _index + 1, TotalQuestions)
            : string.Empty;

        public string SessionStatusText
        {
            get
            {
                if (IsLoading)
                {
                    return GetLocalizedString("QuizLoading", "Loading questions...");
                }

                if (HasLoadError)
                {
                    return GetLocalizedString("QuizOpenTriviaFetchFailed", "Unable to load online questions. Use restart to try again.");
                }

                if (IsUsingCachedQuestions)
                {
                    return GetLocalizedString("QuizUsingCachedQuestions", "Using cached online questions.");
                }

                if (IsMultipleChoiceQuestion)
                {
                    return GetLocalizedString("QuizMultipleChoiceHint", "Select the correct answer.");
                }

                return HasActiveQuestion ? AppResources.QuizTrueFalseHint : AppResources.QuizCompleted;
            }
        }

        public string AccuracyText => AccuracyRatio.ToString("P0", CultureInfo.CurrentCulture);

        public string CompletionText => CompletionRatio.ToString("P0", CultureInfo.CurrentCulture);

        public string LifetimeAccuracyText => LifetimeAccuracyRatio.ToString("P0", CultureInfo.CurrentCulture);

        public string LifetimeCompletionText => LifetimeCompletionRatio.ToString("P0", CultureInfo.CurrentCulture);

        public int RemainingQuestions => Math.Max(0, TotalQuestions - AnsweredQuestions);

        public double AccuracyRatio => AnsweredQuestions == 0 ? 0 : (double)CorrectAnswers / AnsweredQuestions;

        public double CompletionRatio => TotalQuestions == 0 ? 0 : (double)AnsweredQuestions / TotalQuestions;

        public double LifetimeAccuracyRatio => LifetimeAnsweredQuestions == 0 ? 0 : (double)LifetimeCorrectAnswers / LifetimeAnsweredQuestions;

        public double LifetimeCompletionRatio => LifetimeAvailableQuestions == 0 ? 0 : (double)LifetimeAnsweredQuestions / LifetimeAvailableQuestions;

        public double CorrectRatio => AnsweredQuestions == 0 ? 0 : (double)CorrectAnswers / AnsweredQuestions;

        public double WrongRatio => AnsweredQuestions == 0 ? 0 : (double)WrongAnswers / AnsweredQuestions;

        public double SkippedRatio => AnsweredQuestions == 0 ? 0 : (double)SkippedQuestions / AnsweredQuestions;

        public int LifetimeSessionsPlayed => SelectedHistory.SessionsPlayed;

        public int LifetimeAnsweredQuestions => SelectedHistory.QuestionsAnswered;

        public int LifetimeCorrectAnswers => SelectedHistory.CorrectAnswers;

        public int LifetimeWrongAnswers => SelectedHistory.WrongAnswers;

        public int LifetimeSkippedQuestions => SelectedHistory.SkippedQuestions;

        public int LifetimeBestStreak => SelectedHistory.BestStreak;

        public int OverallSessionsPlayed => _historyStore.Overall.SessionsPlayed;

        public int OverallAnsweredQuestions => _historyStore.Overall.QuestionsAnswered;

        public string LastPlayedText => string.IsNullOrWhiteSpace(SelectedHistory.LastPlayedUtc)
            ? AppResources.QuizNoHistoryYet
            : DateTime.TryParse(SelectedHistory.LastPlayedUtc, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var parsed)
                ? parsed.ToLocalTime().ToString("g", CultureInfo.CurrentCulture)
                : AppResources.QuizNoHistoryYet;

        public string CurrentQuestionCategory => HasActiveQuestion ? _questions[_index].Category : string.Empty;

        public bool HasQuestionCategory => !string.IsNullOrWhiteSpace(CurrentQuestionCategory);

        public bool HasActiveQuestion => _index < _questions.Length;

        public bool IsBooleanQuestion => HasActiveQuestion && _questions[_index] is IYesNoQuestion;

        public bool IsMultipleChoiceQuestion => HasActiveQuestion && _questions[_index] is IMultipleChoiceQuestion;

        public bool IsLoading
        {
            get => _isLoading;
            private set
            {
                if (_isLoading == value) return;
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public bool IsUsingCachedQuestions
        {
            get => _isUsingCachedQuestions;
            private set
            {
                if (_isUsingCachedQuestions == value) return;
                _isUsingCachedQuestions = value;
                OnPropertyChanged();
            }
        }

        public string LoadErrorMessage
        {
            get => _loadErrorMessage;
            private set
            {
                if (_loadErrorMessage == value) return;
                _loadErrorMessage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasLoadError));
            }
        }

        public bool HasLoadError => !string.IsNullOrWhiteSpace(LoadErrorMessage);

        public ICommand RestartQuizCommand { get; }
        public ICommand ResetHistoryCommand { get; }

        public bool ButtonsEnabled
        {
            get => _buttonsEnabled;
            set
            {
                if (value != _buttonsEnabled)
                {
                    _buttonsEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Answer
        {
            get => _answer;
            private set
            {
                if (value != _answer)
                {
                    _answer = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color FeedbackColor
        {
            get => _feedbackColor;
            private set
            {
                if (value != _feedbackColor)
                {
                    _feedbackColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool HasFeedback => !string.IsNullOrWhiteSpace(Answer);

        public QuizViewModel()
        {
            _catalogs =
            [
                new QuizCatalog("all", AppResources.QuizAllCatalogs, _ => Task.FromResult(new QuizCatalogLoadResult(Shuffle(Italbytz.Exam.Networking.YesNoQuestions.Questions.Concat(Italbytz.Exam.OperatingSystems.YesNoQuestions.Questions).Cast<IQuestion>().ToArray())))),
                new QuizCatalog("networks", AppResources.Networks, _ => Task.FromResult(new QuizCatalogLoadResult(Shuffle(Italbytz.Exam.Networking.YesNoQuestions.Questions.Cast<IQuestion>().ToArray())))),
                new QuizCatalog("operating-systems", AppResources.OperatingSystems, _ => Task.FromResult(new QuizCatalogLoadResult(Shuffle(Italbytz.Exam.OperatingSystems.YesNoQuestions.Questions.Cast<IQuestion>().ToArray())))),
                new QuizCatalog("opentdb-boolean", GetLocalizedString("QuizOpenTriviaBoolean", "Open Trivia DB (True/False)"), cancellationToken => LoadOpenTriviaCatalogAsync("opentdb-boolean", new OpenTriviaDbRequest(Amount: 10, ChoicesType: Choices.Boolean), cancellationToken)),
                new QuizCatalog("opentdb-mixed", GetLocalizedString("QuizOpenTriviaMixed", "Open Trivia DB (Mixed)"), cancellationToken => LoadOpenTriviaCatalogAsync("opentdb-mixed", new OpenTriviaDbRequest(Amount: 10), cancellationToken)),
                new QuizCatalog("opentdb-film", GetLocalizedString("QuizOpenTriviaFilm", "Open Trivia DB: Film"), cancellationToken => LoadOpenTriviaCatalogAsync("opentdb-film", new OpenTriviaDbRequest(Amount: 10, CategoryId: OpenTriviaDbCategories.EntertainmentFilm), cancellationToken)),
                new QuizCatalog("opentdb-games", GetLocalizedString("QuizOpenTriviaGames", "Open Trivia DB: Video Games"), cancellationToken => LoadOpenTriviaCatalogAsync("opentdb-games", new OpenTriviaDbRequest(Amount: 10, CategoryId: OpenTriviaDbCategories.EntertainmentVideoGames), cancellationToken)),
                new QuizCatalog("opentdb-science", GetLocalizedString("QuizOpenTriviaScience", "Open Trivia DB: Science & Nature"), cancellationToken => LoadOpenTriviaCatalogAsync("opentdb-science", new OpenTriviaDbRequest(Amount: 10, CategoryId: OpenTriviaDbCategories.ScienceNature), cancellationToken))
            ];

            LoadHistory();
            LoadCache();
            LoadOpenTriviaSessionToken();
            RestartQuizCommand = new Command(async () => await ResetQuizAsync());
            ResetHistoryCommand = new Command(ResetHistory);
            _ = ResetQuizAsync();
        }

        public void SubmitAnswer(bool value)
        {
            if (!IsBooleanQuestion || _questions[_index] is not IYesNoQuestion question)
            {
                return;
            }

            if (question.Answer == value)
            {
                RegisterCorrectAnswer(AppResources.Right);
                return;
            }

            RegisterWrongAnswer(question.Answer ? AppResources.Right : AppResources.Wrong);
        }

        public void SubmitAnswer(int selectedIndex)
        {
            if (!IsMultipleChoiceQuestion || _questions[_index] is not IMultipleChoiceQuestion question)
            {
                return;
            }

            if (selectedIndex == question.CorrectAnswerIndex)
            {
                RegisterCorrectAnswer(AppResources.Right);
                return;
            }

            RegisterWrongAnswer(question.PossibleAnswers[question.CorrectAnswerIndex]);
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
            ButtonsEnabled = HasActiveQuestion && !IsLoading && !HasLoadError;

            if (!HasActiveQuestion)
            {
                PersistCurrentSession();
            }

            RefreshAnswerOptions();
            RaiseSessionProperties();
        }

        private async Task ResetQuizAsync()
        {
            PersistCurrentSession();

            var loadVersion = ++_catalogLoadVersion;
            SetSessionLoadingState();

            try
            {
                var result = await SelectedCatalog.LoadQuestionsAsync(CancellationToken.None).ConfigureAwait(false);
                if (loadVersion != _catalogLoadVersion)
                {
                    return;
                }

                MainThread.BeginInvokeOnMainThread(() => ApplyLoadedQuestions(result));
            }
            catch
            {
                if (loadVersion != _catalogLoadVersion)
                {
                    return;
                }

                MainThread.BeginInvokeOnMainThread(ApplyLoadFailure);
            }
        }

        private void ResetHistory()
        {
            _historyStore = QuizHistoryStore.CreateDefault();
            SaveHistory();
            RaiseSessionProperties();
        }

        private string SelectedCatalogName => _catalogs[_selectedCatalogIndex].Title;

        private int LifetimeAvailableQuestions => Math.Max(TotalQuestions * Math.Max(LifetimeSessionsPlayed, 1), TotalQuestions);

        private QuizCatalog SelectedCatalog => _catalogs[_selectedCatalogIndex];

        private QuizHistoryEntry SelectedHistory
        {
            get
            {
                if (!_historyStore.Catalogs.TryGetValue(SelectedCatalog.Id, out var entry))
                {
                    entry = new QuizHistoryEntry();
                    _historyStore.Catalogs[SelectedCatalog.Id] = entry;
                }

                return entry;
            }
        }

        private void RegisterCorrectAnswer(string answerText)
        {
            CorrectAnswers++;
            CurrentStreak++;
            BestStreak = Math.Max(BestStreak, CurrentStreak);
            Answer = answerText;
            FeedbackColor = Color.FromArgb("#0F9D58");
            RaiseSessionProperties();
        }

        private void RegisterWrongAnswer(string correctAnswer)
        {
            WrongAnswers++;
            CurrentStreak = 0;
            Answer = string.Format(AppResources.QuizCorrectAnswer, correctAnswer);
            FeedbackColor = Color.FromArgb("#D93025");
            RaiseSessionProperties();
        }

        private void SetSessionLoadingState()
        {
            _questions = Array.Empty<IQuestion>();
            _index = 0;
            CorrectAnswers = 0;
            WrongAnswers = 0;
            SkippedQuestions = 0;
            CurrentStreak = 0;
            BestStreak = 0;
            Answer = string.Empty;
            FeedbackColor = Colors.Transparent;
            LoadErrorMessage = string.Empty;
            IsUsingCachedQuestions = false;
            IsLoading = true;
            ButtonsEnabled = false;
            _answerOptions = Array.Empty<QuizAnswerOption>();
            RaiseSessionProperties();
        }

        private void ApplyLoadedQuestions(QuizCatalogLoadResult result)
        {
            _questions = result.Questions;
            _index = 0;
            CorrectAnswers = 0;
            WrongAnswers = 0;
            SkippedQuestions = 0;
            CurrentStreak = 0;
            BestStreak = 0;
            Answer = string.Empty;
            FeedbackColor = Colors.Transparent;
            LoadErrorMessage = string.Empty;
            IsUsingCachedQuestions = result.FromCache;
            IsLoading = false;
            ButtonsEnabled = HasActiveQuestion;
            RefreshAnswerOptions();
            RaiseSessionProperties();
        }

        private void ApplyLoadFailure()
        {
            _questions = Array.Empty<IQuestion>();
            _index = 0;
            CorrectAnswers = 0;
            WrongAnswers = 0;
            SkippedQuestions = 0;
            CurrentStreak = 0;
            BestStreak = 0;
            Answer = string.Empty;
            FeedbackColor = Colors.Transparent;
            IsLoading = false;
            IsUsingCachedQuestions = false;
            LoadErrorMessage = GetLocalizedString("QuizOpenTriviaFetchFailed", "Unable to load online questions. Use restart to try again.");
            ButtonsEnabled = false;
            _answerOptions = Array.Empty<QuizAnswerOption>();
            RaiseSessionProperties();
        }

        private async Task<QuizCatalogLoadResult> LoadOpenTriviaCatalogAsync(string cacheKey, OpenTriviaDbRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await FetchOpenTriviaQuestionsAsync(request, cancellationToken).ConfigureAwait(false);
                if (result.IsSuccess && result.Questions.Length > 0)
                {
                    SaveCachedCatalog(cacheKey, result.RawResponse);
                    return new QuizCatalogLoadResult(Shuffle(result.Questions.ToArray()));
                }
            }
            catch
            {
            }

            if (TryLoadCachedCatalog(cacheKey, out var cachedQuestions))
            {
                return new QuizCatalogLoadResult(Shuffle(cachedQuestions), FromCache: true);
            }

            throw new InvalidOperationException(GetLocalizedString("QuizOpenTriviaFetchFailed", "Unable to load online questions. Use restart to try again."));
        }

        private async Task<OpenTriviaDbFetchResult> FetchOpenTriviaQuestionsAsync(OpenTriviaDbRequest request, CancellationToken cancellationToken)
        {
            var requestWithToken = request with { SessionToken = await EnsureOpenTriviaSessionTokenAsync(cancellationToken).ConfigureAwait(false) };
            var result = await _openTriviaClient.GetQuestionsAsync(requestWithToken, cancellationToken).ConfigureAwait(false);

            if (result.ResponseCode is OpenTriviaDbResponseCode.TokenEmpty or OpenTriviaDbResponseCode.TokenNotFound)
            {
                _openTriviaSessionToken = await _openTriviaClient.ResetSessionTokenAsync(_openTriviaSessionToken, cancellationToken).ConfigureAwait(false) ?? string.Empty;
                SaveOpenTriviaSessionToken();

                if (!string.IsNullOrWhiteSpace(_openTriviaSessionToken))
                {
                    result = await _openTriviaClient.GetQuestionsAsync(request with { SessionToken = _openTriviaSessionToken }, cancellationToken).ConfigureAwait(false);
                }
            }

            return result;
        }

        private async Task<string?> EnsureOpenTriviaSessionTokenAsync(CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(_openTriviaSessionToken))
            {
                return _openTriviaSessionToken;
            }

            _openTriviaSessionToken = await _openTriviaClient.GetSessionTokenAsync(cancellationToken).ConfigureAwait(false) ?? string.Empty;
            SaveOpenTriviaSessionToken();
            return string.IsNullOrWhiteSpace(_openTriviaSessionToken) ? null : _openTriviaSessionToken;
        }

        private void PersistCurrentSession()
        {
            if (AnsweredQuestions == 0)
            {
                return;
            }

            UpdateHistoryEntry(SelectedHistory);
            UpdateHistoryEntry(_historyStore.Overall);
            SaveHistory();
        }

        private void UpdateHistoryEntry(QuizHistoryEntry entry)
        {
            entry.SessionsPlayed++;
            entry.QuestionsAnswered += AnsweredQuestions;
            entry.CorrectAnswers += CorrectAnswers;
            entry.WrongAnswers += WrongAnswers;
            entry.SkippedQuestions += SkippedQuestions;
            entry.BestStreak = Math.Max(entry.BestStreak, BestStreak);
            entry.LastPlayedUtc = DateTime.UtcNow.ToString("O", CultureInfo.InvariantCulture);
        }

        private void LoadHistory()
        {
            var serialized = Preferences.Get(QuizHistoryPreferenceKey, string.Empty);
            if (string.IsNullOrWhiteSpace(serialized))
            {
                _historyStore = QuizHistoryStore.CreateDefault();
                return;
            }

            try
            {
                _historyStore = JsonSerializer.Deserialize<QuizHistoryStore>(serialized) ?? QuizHistoryStore.CreateDefault();
                _historyStore.Catalogs ??= new Dictionary<string, QuizHistoryEntry>(StringComparer.Ordinal);
                _historyStore.Overall ??= new QuizHistoryEntry();
            }
            catch
            {
                _historyStore = QuizHistoryStore.CreateDefault();
            }
        }

        private void SaveHistory()
        {
            var serialized = JsonSerializer.Serialize(_historyStore);
            Preferences.Set(QuizHistoryPreferenceKey, serialized);
        }

        private void LoadCache()
        {
            var serialized = Preferences.Get(QuizCachePreferenceKey, string.Empty);
            if (string.IsNullOrWhiteSpace(serialized))
            {
                _cacheStore = QuizCacheStore.CreateDefault();
                return;
            }

            try
            {
                _cacheStore = JsonSerializer.Deserialize<QuizCacheStore>(serialized) ?? QuizCacheStore.CreateDefault();
                _cacheStore.Catalogs ??= new Dictionary<string, QuizCacheEntry>(StringComparer.Ordinal);
            }
            catch
            {
                _cacheStore = QuizCacheStore.CreateDefault();
            }
        }

        private void LoadOpenTriviaSessionToken()
        {
            _openTriviaSessionToken = Preferences.Get(QuizOpenTriviaSessionTokenPreferenceKey, string.Empty);
        }

        private void SaveOpenTriviaSessionToken()
        {
            Preferences.Set(QuizOpenTriviaSessionTokenPreferenceKey, _openTriviaSessionToken);
        }

        private void SaveCache()
        {
            var serialized = JsonSerializer.Serialize(_cacheStore);
            Preferences.Set(QuizCachePreferenceKey, serialized);
        }

        private void SaveCachedCatalog(string cacheKey, string rawResponse)
        {
            _cacheStore.Catalogs[cacheKey] = new QuizCacheEntry
            {
                RawResponse = rawResponse,
                CachedAtUtc = DateTime.UtcNow.ToString("O", CultureInfo.InvariantCulture)
            };
            SaveCache();
        }

        private bool TryLoadCachedCatalog(string cacheKey, out IQuestion[] questions)
        {
            questions = Array.Empty<IQuestion>();

            if (!_cacheStore.Catalogs.TryGetValue(cacheKey, out var cacheEntry) || string.IsNullOrWhiteSpace(cacheEntry.RawResponse))
            {
                return false;
            }

            try
            {
                var result = OpenTriviaDbClient.ParseQuestions(cacheEntry.RawResponse);
                if (!result.IsSuccess || result.Questions.Length == 0)
                {
                    return false;
                }

                questions = result.Questions.ToArray();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private IQuestion[] Shuffle(IQuestion[] questions)
        {
            for (var index = questions.Length - 1; index > 0; index--)
            {
                var swapIndex = _random.Next(index + 1);
                (questions[index], questions[swapIndex]) = (questions[swapIndex], questions[index]);
            }

            return questions;
        }

        private void RefreshAnswerOptions()
        {
            if (!IsMultipleChoiceQuestion || _questions[_index] is not IMultipleChoiceQuestion question)
            {
                _answerOptions = Array.Empty<QuizAnswerOption>();
                OnPropertyChanged(nameof(AnswerOptions));
                return;
            }

            _answerOptions = question.PossibleAnswers
                .Select((answer, index) => new QuizAnswerOption { Index = index, Text = answer })
                .ToArray();
            OnPropertyChanged(nameof(AnswerOptions));
        }

        private static string GetLocalizedString(string key, string fallback)
        {
            var value = AppResources.ResourceManager.GetString(key, CultureInfo.CurrentUICulture);
            return string.IsNullOrWhiteSpace(value) ? fallback : value;
        }

        private void RaiseSessionProperties()
        {
            OnPropertyChanged(nameof(CatalogNames));
            OnPropertyChanged(nameof(AnsweredQuestions));
            OnPropertyChanged(nameof(RemainingQuestions));
            OnPropertyChanged(nameof(Question));
            OnPropertyChanged(nameof(CatalogSummaryText));
            OnPropertyChanged(nameof(QuestionNumberText));
            OnPropertyChanged(nameof(SessionStatusText));
            OnPropertyChanged(nameof(AccuracyText));
            OnPropertyChanged(nameof(CompletionText));
            OnPropertyChanged(nameof(LifetimeAccuracyText));
            OnPropertyChanged(nameof(LifetimeCompletionText));
            OnPropertyChanged(nameof(AccuracyRatio));
            OnPropertyChanged(nameof(CompletionRatio));
            OnPropertyChanged(nameof(LifetimeAccuracyRatio));
            OnPropertyChanged(nameof(LifetimeCompletionRatio));
            OnPropertyChanged(nameof(CorrectRatio));
            OnPropertyChanged(nameof(WrongRatio));
            OnPropertyChanged(nameof(SkippedRatio));
            OnPropertyChanged(nameof(LifetimeSessionsPlayed));
            OnPropertyChanged(nameof(LifetimeAnsweredQuestions));
            OnPropertyChanged(nameof(LifetimeCorrectAnswers));
            OnPropertyChanged(nameof(LifetimeWrongAnswers));
            OnPropertyChanged(nameof(LifetimeSkippedQuestions));
            OnPropertyChanged(nameof(LifetimeBestStreak));
            OnPropertyChanged(nameof(OverallSessionsPlayed));
            OnPropertyChanged(nameof(OverallAnsweredQuestions));
            OnPropertyChanged(nameof(LastPlayedText));
            OnPropertyChanged(nameof(HasActiveQuestion));
            OnPropertyChanged(nameof(HasFeedback));
            OnPropertyChanged(nameof(CurrentQuestionCategory));
            OnPropertyChanged(nameof(HasQuestionCategory));
            OnPropertyChanged(nameof(IsBooleanQuestion));
            OnPropertyChanged(nameof(IsMultipleChoiceQuestion));
            OnPropertyChanged(nameof(HasLoadError));
            OnPropertyChanged(nameof(LoadErrorMessage));
        }
    }
}

