using System;
using System.Collections.ObjectModel;
using ISDCompanion.Resx;

namespace ISDCompanion
{
    public class MensaViewModel
    {
        public ObservableCollection<SectionViewModel<IMeal>> Meals { get; set; } 

        public MensaViewModel()
        {
            Meals = new ObservableCollection<SectionViewModel<IMeal>>();
            var mainDishes = new SectionViewModel<IMeal>()
            {
                LongName = AppResources.Maindishes
            };
            var soups = new SectionViewModel<IMeal>()
            {
                LongName = AppResources.Soups
            };
            var sideDishes = new SectionViewModel<IMeal>()
            {
                LongName = AppResources.Sidedishes
            };
            var desserts = new SectionViewModel<IMeal>()
            {
                LongName = AppResources.Desserts
            };
            Meals.Add(mainDishes);
            Meals.Add(soups);
            Meals.Add(sideDishes);
            Meals.Add(desserts);

            mainDishes.Add(new Meal() {
                Name = "Ochsenschwanzsuppe",
                Image = "http://www.studentenwerk-pb.de/fileadmin/imports/images/speiseleitsystem/7244.jpg",
                Price = "23,5"
            });

            
        }
    }
}
