using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Italbytz.Ports.Common;
using Italbytz.Ports.Meal;
using Mensa.Infrastructure.STWPB;

namespace Mensa.Infrastructure.Adapter
{
    public class MealRepository : IDataSource<int, IMeal>
    {
        private readonly string _language;

        public MealRepository(string language)
        {
            _language = language;
        }

        public Task<Result<IMeal>> Retrieve(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<IMeal>>> RetrieveAll()
        {
            try
            {
                var api = new MensaAPI(_language);
                var meals = await api.GetTodaysHammMeals();                
                return new Result<List<IMeal>>(meals.Select((meal) => (IMeal)meal.ToMeal()).ToList());
            }
            catch (System.Exception ex)
            {
                return new Result<List<IMeal>>(ex);
            }

        }

    }
}
