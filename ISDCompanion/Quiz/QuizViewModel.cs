using System;
using System.Collections.Generic;
using TriviaPorts;

namespace ISDCompanion.Quiz
{
    public class QuizViewModel
    {
        private readonly List<IQuestion> questions = new List<IQuestion> { new YesNoQuestion() { } };

        public QuizViewModel()
        {
        }

        private class YesNoQuestion : IYesNoQuestion
        {
            public bool Answer { get; set;}
            public string Category { get; set;}
            public QuestionType QuestionType { get; set;}
            public Choices ChoicesType { get; set;}
            public Difficulty Difficulty { get; set;}
            public string Text { get; set;}
            public IQuestion AlternativeQuestion { get; set;}
        }
    }
}
