using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ISDCompanion.Enums;
using ISDCompanion.Helpers;
using ISDCompanion.Services;
using Italbytz.Adapters.Exam.OperatingSystems;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class StatusViewModel
    {
        public Command SelectedGuestCommand { get; }
        public Command SelectedStudentCommand { get; }
        public Command SelectedServantCommand { get; }



        private string requests;
        public string Requests
        {
            get => requests;
            set
            {
                if (value != requests)
                {
                    requests = value;
                }
            }
        }

        public StatusViewModel()
        {
            SelectedGuestCommand = new Command(OnSelectedGuest);
            SelectedStudentCommand = new Command(OnSelectedStudent);
            SelectedServantCommand = new Command(OnSelectedServant);
        }
        private void OnSelectedGuest(object obj)
        {
            //Eintrag in DB speichern => ID 1
            Settings.Status = (int)StatusType.Guest;
            Settings.Semester = 1;
            Settings.WelcomeStatus = (int)WelcomeStatusType.Finished;

            Application.Current.MainPage = new AppShell();

            var nav = App.Current.MainPage as Xamarin.Forms.Shell;
            nav.BackgroundColor = Color.Black;
        }

        private void OnSelectedStudent(object obj)
        {
            //Eintrag in DB speichern
            Settings.Status = (int)StatusType.Student;

            Application.Current.MainPage = new SemesterPage();
        }

        private void OnSelectedServant(object obj)
        {
            //Eintrag in DB speichern  => ID 99
            Settings.Status = (int)StatusType.Emmployee;
            Settings.Semester = 5;
            Settings.WelcomeStatus = (int)WelcomeStatusType.Finished;

            Application.Current.MainPage = new AppShell();

            var nav = App.Current.MainPage as Xamarin.Forms.Shell;
            nav.BackgroundColor = Color.Black;
        }
    }
}
