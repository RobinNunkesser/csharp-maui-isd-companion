using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ISDCompanion.Resx;
using Italbytz.Extensions;
using Italbytz.Ports.Trivia;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class QuizViewModel : INotifyPropertyChanged
    {
        private readonly IQuestion[] shuffledQuestions;
        private Random _random = new();

        public ICommand AnswerCommand { get; private set; }
        public ICommand SkipCommand { get; private set; }


        public QuizViewModel(IQuestion[] questions)
        {
            shuffledQuestions = questions;
            _random.Shuffle(shuffledQuestions);
            OnPropertyChanged(nameof(Question));
            AnswerCommand = new Command<bool>(EvaluateAnswer);
            SkipCommand = new Command(Skip);
        }

        public string Question => shuffledQuestions[index].Text;

        private string answer;
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

        void EvaluateAnswer(bool value)
        {
            if (shuffledQuestions[index] is IYesNoQuestion)
            {
                if (((IYesNoQuestion)shuffledQuestions[index]).Answer == value)
                {
                    Answer = AppResources.Right;
                }
                else
                {
                    Answer = AppResources.Wrong;
                }
            }
            IncreaseIndex();
        }

        void Skip()
        {            
            IncreaseIndex();
        }

        void IncreaseIndex()
        {
            Index = (Index + 1) % shuffledQuestions.Length;
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion

        
    }

}
