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
        public View Table
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
        private View _Table { get; set; }


        public View Table_Header
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
        private View _Table_Header { get; set; }


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


        public delegate void ScrollToPositionAction(int x, int y, bool isAnimated);
        public event ScrollToPositionAction ScrollToPosition;


        protected override void Initialize()
        {
            ButtonNewExercise = new Command(newExercise, () => true);
            ButtonNextStep = new Command(nextStep, () => true);
            ButtonLastStep = new Command(lastStep, () => true);
            ButtonCompleteSolution = new Command(showCompleteSolution, () => true);
            ButtonInfo = new Command(showInfoAsync, () => true);

        }

        
        public void scroll()
        {
            ScrollToPosition?.Invoke(_TableGenService.X_CoordoninatesOfInterest(), _TableGenService.Y_CoordoninatesOfInterest(), true);
        }


        private void nextStep()
        {
            Table = _TableGenService.GenerateTable_NextStep();
            Info_Text = _TableGenService.GetInfoText();
            Info_Button_Clickable = _TableGenService.InfoAvailable();
            scroll();
        }

        private void lastStep()
        {
            Table = _TableGenService.GenerateTable_PreviousStep();
            Info_Text = _TableGenService.GetInfoText();
            Info_Button_Clickable = _TableGenService.InfoAvailable();
            scroll();
        }

        private void showCompleteSolution()
        {
            Table = _TableGenService.GenerateTable_ShowSolution();
            scroll();
        }

        private async void showInfoAsync()
        {
            await App.Current.MainPage.DisplayAlert("Info", Info_Text, "Ok");
        }
    }
}