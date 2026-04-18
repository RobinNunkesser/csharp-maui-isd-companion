using System;
using Italbytz.Meal.Abstractions;

namespace Italbytz.Adapters.Meal.OpenMensa
{
    public class MockMeal : IMeal
    {
        public MockMeal()
        {
        }

        public DateTime Date { get; set; } = DateTime.Today;
        public string Name { get; set; } = "";
        public string Image { get; set; } = "";
        public Allergens Allergens { get; set; }
        public Additives Additives { get; set; }
        public Category Category { get; set; }
        public Badge[] Badges { get; set; } = Array.Empty<Badge>();
        public IPrice Price { get; set; } = new MockPrice();
    }
}

