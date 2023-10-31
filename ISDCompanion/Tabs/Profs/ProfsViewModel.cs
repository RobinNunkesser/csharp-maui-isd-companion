using System.Windows.Input;

namespace StudyCompanion
{
    public class ProfsViewModel
    {
        public ICommand ProfCommand { get; set; }

        public ProfsViewModel(INavigation navigation)
        {
            ProfCommand = new Command<string>(async (string source) =>
            {
                await navigation.PushAsync(new ProfPage(source));
            });
        }
    }
}
