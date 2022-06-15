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
    public class FirstOnInstallViewModel
    {
        public Command CreateNewAccountCommand { get; }


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

        public FirstOnInstallViewModel()
        {
            CreateNewAccountCommand = new Command(OnCreateNewAccount);
        }


        private void OnCreateNewAccount(object obj)
        {
            Application.Current.MainPage = new AppShell();
            TheTheme.SetTheme();

            var nav = App.Current.MainPage as Xamarin.Forms.Shell;
            nav.BackgroundColor = Color.Black;
        }
    }
}
