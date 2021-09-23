using System;
using System.Collections.Generic;
using ISDCompanion.Resx;
using Mensa.Core;
using Mensa.Core.Ports;
using Mensa.Infrastructure.Adapter;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ISDCompanion
{
    public partial class MensaPage : ContentPage
    {
        private IGetMealsService service = new GetMealsService(new MealRepository(LocalizationResourceManager.Current.CurrentCulture.TwoLetterISOLanguageName));
        private MensaViewModel viewModel = new MensaViewModel();

        public MensaPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            service.Execute("", Success, Error);
        }

        private async void Success(List<MealPorts.IMeal> meals)
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
