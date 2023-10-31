using System;
namespace StudyCompanion
{
    public class ExerciseStepViewModel : ViewModel
    {
        private bool visible = false;
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
                OnPropertyChanged();
            }
        }

        public ExerciseStepViewModel()
        {
        }
    }
}

