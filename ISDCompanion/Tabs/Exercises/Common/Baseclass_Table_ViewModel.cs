namespace StudyCompanion
{
    public abstract class Baseclass_Table_ViewModel : StepwiseExerciseViewModel
    {
        public View Exercise_Header
        {
            get
            {
                return _Exercise_Header;
            }
            set
            {
                _Exercise_Header = value;
                OnPropertyChanged();
            }
        }
        private View _Exercise_Header { get; set; }

        public View Exercise_Content
        {
            get
            {
                return _Exercise_Content;
            }
            set
            {
                _Exercise_Content = value;
                OnPropertyChanged();
            }
        }
        private View _Exercise_Content { get; set; }


        public View Exercise_Content_Header
        {
            get
            {
                return _Exercise_Content_Header;
            }
            set
            {
                _Exercise_Content_Header = value;
                OnPropertyChanged();
            }
        }
        private View _Exercise_Content_Header { get; set; }


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


        public void scroll()
        {
            ScrollToPosition?.Invoke(_TableGenService.X_CoordoninatesOfInterest(), _TableGenService.Y_CoordoninatesOfInterest(), true);
        }


        protected override void nextStep()
        {
            Exercise_Content = _TableGenService.GenerateTable_NextStep();
            Info_Text = _TableGenService.GetInfoText();
            Info_Button_Clickable = _TableGenService.InfoAvailable();
            scroll();
        }

        protected override void previousStep()
        {
            Exercise_Content = _TableGenService.GenerateTable_PreviousStep();
            Info_Text = _TableGenService.GetInfoText();
            Info_Button_Clickable = _TableGenService.InfoAvailable();
            scroll();
        }

        protected override void showCompleteSolution()
        {
            Exercise_Content = _TableGenService.GenerateTable_ShowSolution();
            Info_Button_Clickable = _TableGenService.InfoAvailable();
            scroll();
        }

        protected override async void showInfo()
        {
            await App.Current.MainPage.DisplayAlert("Info", Info_Text, "Ok");
        }
    }
}