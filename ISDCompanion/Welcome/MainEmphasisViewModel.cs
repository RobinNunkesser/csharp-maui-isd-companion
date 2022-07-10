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
    public class MainEmphasisViewModel
    {
        public Command SelectedMobileComputingCommand { get; }
        public Command SelectedCyberSecurityCommand { get; }
        public Command SelectedEmbeddedSystemsCommand { get; }
        public Command SelectedNoneCommand { get; }


        public MainEmphasisViewModel()
        {
            SelectedMobileComputingCommand = new Command(OnSelectedMobileComputing);
            SelectedCyberSecurityCommand = new Command(OnSelectedCyberSecurity);
            SelectedEmbeddedSystemsCommand = new Command(OnSelectedEmbeddedSystems);
            SelectedNoneCommand = new Command(OnSelectedNone);
        }

        private void OnSelectedMobileComputing(object obj)
        {
            Settings.Status = (int)MainEmphasisType.MobileComputing;

            //Eintrag in DB speichern
            Application.Current.MainPage = new ModulePage();
        }
        private void OnSelectedCyberSecurity(object obj)
        {
            Settings.Status = (int)MainEmphasisType.CyberSecurity;

            //Eintrag in DB speichern
            Application.Current.MainPage = new ModulePage();
        }
        private void OnSelectedEmbeddedSystems(object obj)
        {
            Settings.Status = (int)MainEmphasisType.EmbeddedSystems;

            //Eintrag in DB speichern
            Application.Current.MainPage = new ModulePage();
        }
        private void OnSelectedNone(object obj)
        {
            Settings.Status = (int)MainEmphasisType.None;

            //Eintrag in DB speichern
            Application.Current.MainPage = new ModulePage();
        }

    }
}
