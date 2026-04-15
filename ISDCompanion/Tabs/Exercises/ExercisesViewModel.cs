using System.Windows.Input;

namespace StudyCompanion
{
    public class ExercisesViewModel
    {
        public ICommand NavigateCommand { get; set; }

        public ExercisesViewModel(INavigation navigation)
        {
            NavigateCommand = new Command<Type>(async (Type pageType) =>
            {
                if (!typeof(Page).IsAssignableFrom(pageType)) return;
                if (Activator.CreateInstance(pageType) is not Page page) return;
                await navigation.PushAsync(page);
            });
        }
    }
}
