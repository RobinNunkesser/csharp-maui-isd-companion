using System;
using System.Collections.Generic;
using ISDCompanion.Enums;
using ISDCompanion.Resx;
using Italbytz.Adapters.Meal.STWPB;
using Italbytz.Ports.Meal;
using Mensa.Core;
using Mensa.Core.Ports;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ISDCompanion
{
    public partial class MensaPage : ContentPage
    {
        private readonly Mensa.Core.Ports.IGetMealsService service;
        private readonly MensaViewModel viewModel = new();

        public MensaPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
            var repository = new MealRepository(Secrets.id, LocalizationResourceManager.Current.CurrentCulture.TwoLetterISOLanguageName);
            service = new GetMealsService(repository);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                Success(await service.Execute());
            }
            catch (Exception ex)
            {
                Error(ex);
            }
        }

        private async void Success(List<IMeal> meals)
        {
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
