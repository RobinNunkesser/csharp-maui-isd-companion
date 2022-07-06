using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ISDCompanion.Helpers;
using ISDCompanion.Services;
using Italbytz.Adapters.Exam.OperatingSystems;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class SemesterViewModel : INotifyPropertyChanged
    {
        public Command SelectedSemesterCommand { get; }

        private string semesterValueText;
        public string SemesterValueText
        {
            get
            {
                return semesterValueText;
            }
            set
            {
                semesterValueText = value;
                OnPropertyChanged(nameof(SemesterValueText));
            }
        }

        private int semesterValue;
        public int SemesterValue
        {
            get
            {
                if (semesterValue == 0)
                {
                    SemesterValue = 1;
                }
                return semesterValue;
            }
            set
            {
                semesterValue = value;
                SemesterValueText = semesterValue.ToString();
                OnPropertyChanged(nameof(SemesterValue));
            }
        }

        public SemesterViewModel()
        {
            SelectedSemesterCommand = new Command(OnSelectedSemester);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnSelectedSemester(object obj)
        {
            //Eintrag in DB speichern
            Settings.Semester = SemesterValue;

            Application.Current.MainPage = new MainEmphasisPage();
        }

    }
}
