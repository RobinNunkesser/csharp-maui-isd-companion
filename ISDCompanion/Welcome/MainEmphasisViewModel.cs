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
    public class MainEmphasisViewModel
    {
        public Command SelectedMainEmphasisCommand { get; }


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

        public MainEmphasisViewModel()
        {
            SelectedMainEmphasisCommand = new Command(OnSelectedMainEmphasis);
        }

        private void OnSelectedMainEmphasis(object obj)
        {
            //Eintrag in DB speichern
            Application.Current.MainPage = new ModulePage();
        }

    }
}
