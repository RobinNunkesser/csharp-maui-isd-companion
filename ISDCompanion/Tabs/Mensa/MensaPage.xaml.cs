using StudyCompanion.Resources.Strings;
using Mensa.Core;
using Mensa.Core.Ports;
using Italbytz.Adapters.Meal.STWPB;
using Italbytz.Ports.Meal;
using System.Globalization;

namespace StudyCompanion
{
    public partial class MensaPage : ContentPage
    {
        private readonly Mensa.Core.Ports.IGetMealsService service;
        private readonly MensaViewModel viewModel = new();

        public MensaPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
            var repository = new MealRepository(Secrets.id, CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
            service = new GetMealsService(repository);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                //var result = await service.Execute();
                Success(service.Execute());
            }
            catch (Exception ex)
            {
                Error(ex);
            }
        }

        private async void Success(Task<List<IMealCollection>> mealsTask)
        {
            var meals = await mealsTask;
            if (Settings.WelcomeStatus == (int)WelcomeStatusType.Unfinished)
            {
                List<string> statusChoices = new List<string> { AppResources.Student, AppResources.Staff, AppResources.Guest };
                string chosenStatus = await DisplayActionSheet(AppResources.StatusQuery, AppResources.Cancel, null, statusChoices.ToArray());
                if (!chosenStatus.Equals(AppResources.Cancel))
                {
                    Settings.WelcomeStatus = (int)WelcomeStatusType.Finished;
                    Settings.Status = statusChoices.IndexOf(chosenStatus);
                }
            }
            if (meals.Count > 0)
            {
                viewModel.SetMeals(meals);
            }
            else
            {
                await DisplayAlert(AppResources.Error, AppResources.NoMeals, AppResources.OK);
            }
        }

        private async void Error(Exception ex)
        {
            await DisplayAlert(AppResources.Error, AppResources.ErrorMessage, AppResources.OK);
        }


        async void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            var result = await DisplayAlert(AppResources.Info, AppResources.MensaInfo, AppResources.Show, AppResources.Cancel);
            if (result) await Launcher.OpenAsync(new Uri("https://www.studierendenwerk-pb.de/gastronomie/speiseplaene/mensa-basilica-hamm"));
        }
    }
}
