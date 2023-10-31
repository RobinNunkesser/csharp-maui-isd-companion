using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using StudyCompanion.Resources.Strings;
using Italbytz.Ports.Meal;
using Italbytz.Adapters.Meal.STWPB;

namespace StudyCompanion
{
    public class MensaViewModel : ViewModel
    {
        public ObservableCollection<SectionViewModel<IMeal>> Meals { get; set; }

        private SectionViewModel<IMeal> mainDishes;
        private SectionViewModel<IMeal> soups;
        private SectionViewModel<IMeal> sideDishes;
        private SectionViewModel<IMeal> desserts;

        public MensaViewModel()
        {
        }

        internal void SetMeals(List<IMeal> meals)
        {
            Meals = new ObservableCollection<SectionViewModel<IMeal>>();
            mainDishes = new SectionViewModel<IMeal>()
            {
                Header = AppResources.Maindishes
            };
            soups = new SectionViewModel<IMeal>()
            {
                Header = AppResources.Soups
            };
            sideDishes = new SectionViewModel<IMeal>()
            {
                Header = AppResources.Sidedishes
            };
            desserts = new SectionViewModel<IMeal>()
            {
                Header = AppResources.Desserts
            };
            Meals.Add(mainDishes);
            Meals.Add(soups);
            Meals.Add(sideDishes);
            Meals.Add(desserts);
            foreach (var meal in meals)
            {
                var excludeMeal = false;
                foreach (Allergens flagToCheck in Enum.GetValues(typeof(Allergens)))
                {
                    if (flagToCheck != Allergens.None &&
                        meal.Allergens.HasFlag(flagToCheck) &&
                        !Settings.Allergens.HasFlag(flagToCheck))
                    {
                        excludeMeal = true;
                        break;
                    }
                }
                foreach (Additives flagToCheck in Enum.GetValues(typeof(Additives)))
                {
                    if (flagToCheck != Additives.None &&
                        meal.Additives.HasFlag(flagToCheck) &&
                        !Settings.Additives.HasFlag(flagToCheck))
                    {
                        excludeMeal = true;
                        break;
                    }
                }
                if (excludeMeal) continue;

                switch (meal.Category)
                {
                    case Category.Dessert:
                        desserts.Add(meal);
                        break;
                    case Category.Dish:
                        mainDishes.Add(meal);
                        break;
                    case Category.Soup:
                        soups.Add(meal);
                        break;
                    case Category.None:
                    case Category.Sidedish:
                    default:
                        sideDishes.Add(meal);
                        break;
                }
            }
            OnPropertyChanged("Meals");
        }
    }

}
