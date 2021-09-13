using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class ExercisesViewModel
    {
        public ICommand NavigateCommand { get; set; }

        public ExercisesViewModel(INavigation navigation)
        {
            this.NavigateCommand = new Command<Type>(async (Type pageType) =>
            {
                Page page = (Page)Activator.CreateInstance(pageType);
                await navigation.PushAsync(page);
            });
        }
        
    }
}
