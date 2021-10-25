using System;
using System.Collections.Generic;
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
        private readonly IGetMealsService service = new GetMealsService(new MealRepository(Secrets.id, LocalizationResourceManager.Current.CurrentCulture.TwoLetterISOLanguageName));
        private readonly MensaViewModel viewModel = new();

        public MensaPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            service.Execute(Success, Error);
        }

        private async void Success(List<IMeal> meals)
        {
            if (meals.Count > 0)
            {
                viewModel.SetMeals(meals);
            } else
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
