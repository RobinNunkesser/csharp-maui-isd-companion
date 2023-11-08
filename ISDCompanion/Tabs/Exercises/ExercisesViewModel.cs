using System.Windows.Input;
using Italbytz.Ports.Trivia;

namespace StudyCompanion
{
    public class ExercisesViewModel
    {
        public ICommand NavigateCommand { get; set; }
        public ICommand NetworksQuizCommand { get; set; }
        public ICommand OpSysQuizCommand { get; set; }

        public ExercisesViewModel(INavigation navigation)
        {
            NavigateCommand = new Command<Type>(async (Type pageType) =>
            {
                Page page = (Page)Activator.CreateInstance(pageType);
                await navigation.PushAsync(page);
            });
            NetworksQuizCommand = new Command(async () =>
            {
                await navigation.PushAsync(new QuizPage(new QuizViewModel(Italbytz.Adapters.Exam.Networks.YesNoQuestions.Questions)));
            });
            OpSysQuizCommand = new Command(async () =>
            {
                await navigation.PushAsync(new QuizPage(new QuizViewModel(Italbytz.Adapters.Exam.OperatingSystems.YesNoQuestions.Questions)));
            });
        }

        private class YesNoQuestion : IYesNoQuestion
        {
            public bool Answer { get; set; }
            public string Category { get; set; }
            public QuestionType QuestionType { get; set; }
            public Choices ChoicesType { get; set; }
            public Difficulty Difficulty { get; set; }
            public string Text { get; set; }
            public IQuestion AlternativeQuestion { get; set; }
        }


    }
}
