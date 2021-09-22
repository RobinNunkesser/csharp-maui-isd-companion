using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonPorts;
using MealPorts;
using Mensa.Infrastructure.STWPB;

namespace Mensa.Infrastructure.Adapter
{
    public class MealRepository : IRepository<int, IMeal>
    {
        public Task<Result<int>> Create(IMeal entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IMeal>> Retrieve(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<IMeal>>> RetrieveAll()
        {
            try
            {
                var api = new MensaAPI();
                var meals = await api.GetTodaysHammMeals();                
                return new Result<List<IMeal>>(meals.Select((meal) => (IMeal)meal.ToMeal()).ToList());
            }
            catch (System.Exception ex)
            {
                return new Result<List<IMeal>>(ex);
            }

        }

        public Task<Result<bool>> Update(int id, IMeal entity)
        {
            throw new NotImplementedException();
        }
    }
}
