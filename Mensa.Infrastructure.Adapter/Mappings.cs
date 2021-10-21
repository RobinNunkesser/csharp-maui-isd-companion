using System;
using System.Globalization;
using Italbytz.Ports.Meal;

namespace Mensa.Infrastructure.Adapter
{
    public static class Mappings
    {
        public static Meal ToMeal(this Mensa.Infrastructure.STWPB.Meal self)
        {
            var name = self.NameDe ?? self.NameEn;            
            var category = self.Category switch
            {
                STWPB.Category.None => Category.None,
                STWPB.Category.Dessert => Category.Dessert,
                STWPB.Category.DessertCounter => Category.Dessert,
                STWPB.Category.Dish => Category.Dish,
                STWPB.Category.DishDefault => Category.Dish,
                STWPB.Category.DishGrill => Category.Dish,
                STWPB.Category.Empty => Category.None,
                STWPB.Category.Sidedish => Category.Sidedish,
                STWPB.Category.Soups => Category.Soup,
                _ => Category.None,
            };

            var allergens = Allergens.None;
            var additives = Additives.None;

            foreach (var allergen in self.Allergens)
            {
                switch (allergen)
                {
                    case STWPB.AllergenEnum.Z1: additives |= Additives.FoodColoring; break;
                    case STWPB.AllergenEnum.Z2: additives |= Additives.Preservatives; break;
                    case STWPB.AllergenEnum.Z3: additives |= Additives.Antioxidants; break;
                    case STWPB.AllergenEnum.Z4: additives |= Additives.FlavorEnhancer; break;
                    case STWPB.AllergenEnum.Z5: additives |= Additives.Phosphate; break;
                    case STWPB.AllergenEnum.Z6: additives |= Additives.Sulphureted; break;
                    case STWPB.AllergenEnum.Z7: additives |= Additives.Waxed; break;
                    case STWPB.AllergenEnum.Z8: additives |= Additives.Blackend; break;
                    case STWPB.AllergenEnum.Z9: additives |= Additives.Sweetener; break;
                    case STWPB.AllergenEnum.Z10: additives |= Additives.Phenylalanine; break;
                    case STWPB.AllergenEnum.Z11: additives |= Additives.Taurine; break;
                    case STWPB.AllergenEnum.Z12: additives |= Additives.NitritePicklingSalt; break;
                    case STWPB.AllergenEnum.Z13: additives |= Additives.Caffeinated; break;
                    case STWPB.AllergenEnum.Z14: additives |= Additives.Quinine; break;
                    case STWPB.AllergenEnum.Z15: additives |= Additives.MilkProtein; break;
                    case STWPB.AllergenEnum.A1: allergens |= Allergens.Gluten; 
                        break;
                    case STWPB.AllergenEnum.A2: allergens |= Allergens.Shellfish; 
                        break;
                    case STWPB.AllergenEnum.A3:
                        allergens |= Allergens.Eggs;
                        break;
                    case STWPB.AllergenEnum.A4:
                        allergens |= Allergens.Fish;
                        break;
                    case STWPB.AllergenEnum.A5:
                        allergens |= Allergens.Peanuts;
                        break;
                    case STWPB.AllergenEnum.A6:
                        allergens |= Allergens.Soy;
                        break;
                    case STWPB.AllergenEnum.A7:
                        allergens |= Allergens.Milk;
                        break;
                    case STWPB.AllergenEnum.A8:
                        allergens |= Allergens.Nuts;
                        break;
                    case STWPB.AllergenEnum.A9:
                        allergens |= Allergens.Celery;
                        break;
                    case STWPB.AllergenEnum.A10:
                        allergens |= Allergens.Mustard;
                        break;
                    case STWPB.AllergenEnum.A11:
                        allergens |= Allergens.Sesame;
                        break;
                    case STWPB.AllergenEnum.A12:
                        allergens |= Allergens.Sulfur;
                        break;
                    case STWPB.AllergenEnum.A13:
                        allergens |= Allergens.Lupine;
                        break;
                    case STWPB.AllergenEnum.A14:
                        allergens |= Allergens.Mollusk;
                        break;
                    default:
                        break;
                }
            }

            var cultureInfo = CultureInfo.GetCultureInfo("de-DE");
            var price = String.Format(cultureInfo, "{0:C} / {1:C} / {2:C}", self.PriceStudents, self.PriceWorkers, self.PriceGuests);

            var meal = new Meal()
            {
                Name = name,
                Image = self.Image,
                Category = category,
                Additives = additives,
                Allergens = allergens,
                Price = price
            };

            return meal;
        }

    }
}
