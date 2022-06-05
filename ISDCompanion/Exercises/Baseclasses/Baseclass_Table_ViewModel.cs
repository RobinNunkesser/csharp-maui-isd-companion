using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using ISDCompanion.Services;
using Italbytz.Adapters.Exam.OperatingSystems;
using Xamarin.Forms;

namespace ISDCompanion
{
    public abstract class Baseclass_Table_ViewModel : ExerciseViewModel
    {
        public Grid Table
        {
            get
            {
                return _Table;
            }
            set
            {
                _Table = value;
                OnPropertyChanged();
            }
        }
        private Grid _Table { get; set; }


        public Grid Table_Header
        {
            get
            {
                return _Table_Header;
            }
            set
            {
                _Table_Header = value;
                OnPropertyChanged();
            }
        }
        private Grid _Table_Header { get; set; }


        public String Info_Text
        {
            get
            {
                return _Info_Text;
            }
            set
            {
                _Info_Text = value;
                OnPropertyChanged();
            }
        }
        private String _Info_Text { get; set; }

        public bool Info_Button_Clickable
        {
            get
            {
                return _Info_Button_Clickable;
            }
            set
            {
                _Info_Button_Clickable = value;
                OnPropertyChanged();
            }
        }
        private bool _Info_Button_Clickable { get; set; }


        public ITableGenService _TableGenService { get; set; }

        public ICommand ButtonNextStep { set; get; }
        public ICommand ButtonLastStep { set; get; }
        public ICommand ButtonCompleteSolution { set; get; }
        public ICommand ButtonInfo { set; get; }
        public ICommand ButtonNewExercise { set; get; }


        protected override void Initialize()
        {
            ButtonNewExercise = new Command(newExercise, () => true);
            ButtonNextStep = new Command(nextStep, () => true);
            ButtonLastStep = new Command(lastStep, () => true);
            ButtonCompleteSolution = new Command(showCompleteSolution, () => true);
            ButtonInfo = new Command(showInfoAsync, () => true);

            newExercise();
        }

        protected abstract void newExercise();

        private void nextStep()
        {
            Table = _TableGenService.GenerateTable_NextStep();
            Info_Text = _TableGenService.GetInfoText();
            Info_Button_Clickable = _TableGenService.InfoAvailable();
        }

        private void lastStep()
        {
            Table = _TableGenService.GenerateTable_PreviousStep();
            Info_Text = _TableGenService.GetInfoText();
            Info_Button_Clickable = _TableGenService.InfoAvailable();
        }

        private void showCompleteSolution()
        {
            Table = _TableGenService.GenerateTable_ShowSolution();
        }

        private async void showInfoAsync()
        {
            await App.Current.MainPage.DisplayAlert("Info", Info_Text, "Ok");
        }
    }
}