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
    public class ModuleViewModel
    {
        public Command SubmitCommand { get; }



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

        public ModuleViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }

        private void OnSubmit(object obj)
        {
            //Auswahl speichern
            //---> Konzept überlegen

            Application.Current.MainPage = new AppShell();
            TheTheme.SetTheme();

            var nav = App.Current.MainPage as Xamarin.Forms.Shell;
            nav.BackgroundColor = Color.Black;
        }
    }
}
