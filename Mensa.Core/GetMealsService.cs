using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Italbytz.Adapters.Meal;
using Italbytz.Ports.Common;
using Italbytz.Ports.Meal;
using Mensa.Core.Ports;

namespace Mensa.Core
{
    public class GetMealsService : Ports.IGetMealsService
    {
        private readonly IDataSource<int, IMeal> _repository;
        private static readonly string format = "yyyy-MM-dd";
        private string lastSuccess = null;
        private List<IMeal> lastMeals;

        public GetMealsService(IDataSource<int, IMeal> repository)
        {
            _repository = repository;
        }

        public async Task<List<IMealCollection>> Execute()
        {
            if (DateTime.Now.ToString(format) != lastSuccess)
            {
                var meals = await _repository.RetrieveAll();
                lastSuccess = DateTime.Now.ToString(format);
                lastMeals = meals;
            }
            var collectionsList = new List<IMealCollection>();
            var collections = lastMeals.GroupBy(meal => meal.Category);
            foreach (var collection in collections)
            {
                var mealCollection = new MealCollection()
                {
                    Category = collection.Key
                };
                foreach (var meal in collection)
                {
                    mealCollection.Meals.Add(meal);
                }
                collectionsList.Add(mealCollection);
            }
            return collectionsList;
        }
    }
}