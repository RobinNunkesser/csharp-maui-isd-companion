using System;
namespace ISDCompanion
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

