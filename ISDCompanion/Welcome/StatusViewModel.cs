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
    public class StatusViewModel
    {
        public Command SelectedGuestCommand { get; }
        public Command SelectedStudentCommand { get; }
        public Command SelectedServant { get; }



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
            SelectedStudentCommand = new Command(OnSelectedStudent);
        }


        private void OnSelectedStudent(object obj)
        {
            Application.Current.MainPage = new AppShell();
            TheTheme.SetTheme();

            var nav = App.Current.MainPage as Xamarin.Forms.Shell;
            nav.BackgroundColor = Color.Black;
        }

        //private void OnSelectedStudent(object obj)
        //{
        //    Application.Current.MainPage = new AppShell();
        //    TheTheme.SetTheme();

        //    var nav = App.Current.MainPage as Xamarin.Forms.Shell;
        //    nav.BackgroundColor = Color.Black;
        //}
    }
}
