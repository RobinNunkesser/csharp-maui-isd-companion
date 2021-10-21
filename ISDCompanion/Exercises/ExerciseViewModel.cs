using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ISDCompanion
{
    public abstract class ExerciseViewModel : ViewModel
    {
        public ICommand NewParams { get; set; }

        public ExerciseViewModel()
        {
            Initialize();
            NewParams = new Command(Initialize);
        }

        protected abstract void Initialize();
    }
}

