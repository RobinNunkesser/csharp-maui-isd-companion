using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RandomExtensions;
using TriviaPorts;

namespace ISDCompanion
{
    public class QuizViewModel : INotifyPropertyChanged
    {
        private readonly List<IQuestion> questions = new List<IQuestion> { new YesNoQuestion() { } };
        private readonly IQuestion[] shuffledQuestions;
        private Random _random = new();

        public QuizViewModel()
        {
            questions.Add(new YesNoQuestion()
            {
                Answer = true,
                Text = "Donkey"
            });
            shuffledQuestions = questions.ToArray();
            _random.Shuffle(shuffledQuestions);
            OnPropertyChanged(nameof(Question));
        }

        public string Question => shuffledQuestions[index].Text;

        private int index;
        public int Index
        {
            get => index;
            set
            {
                if (value != index)
                {
                    index = value;
                    OnPropertyChanged(nameof(Question));
                }
            }
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion

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
