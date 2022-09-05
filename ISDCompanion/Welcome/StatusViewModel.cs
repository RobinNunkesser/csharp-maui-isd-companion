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
            Settings.Status = (int)StatusType.Guest;
            Settings.WelcomeStatus = (int)WelcomeStatusType.Finished;

            Application.Current.MainPage = new AppShell();
            TheTheme.SetTheme();

            var nav = App.Current.MainPage as Xamarin.Forms.Shell;
            nav.BackgroundColor = Color.Black;
        }

        private void OnSelectedStudent(object obj)
        {
            Settings.Status = (int)StatusType.Student;
            Settings.WelcomeStatus = (int)WelcomeStatusType.Finished;

            Application.Current.MainPage = new AppShell();
            TheTheme.SetTheme();

            var nav = App.Current.MainPage as Xamarin.Forms.Shell;
            nav.BackgroundColor = Color.Black;
        }

        private void OnSelectedServant(object obj)
        {
            Settings.Status = (int)StatusType.Emmployee;
            Settings.WelcomeStatus = (int)WelcomeStatusType.Finished;

            Application.Current.MainPage = new AppShell();
            TheTheme.SetTheme();

            var nav = App.Current.MainPage as Xamarin.Forms.Shell;
            nav.BackgroundColor = Color.Black;
        }
    }
}
