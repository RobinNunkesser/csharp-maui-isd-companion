using System;
namespace Mensa.Infrastructure.Adapter
{
    public static class Mappings
    {
        public static Meal ToMeal(this Mensa.Infrastructure.STWPB.Meal self)
        {
            var meal = new Meal()
            {
                Name = self.NameDe,
                Image = self.Image,
            };

            return meal;
        }

    }
}
