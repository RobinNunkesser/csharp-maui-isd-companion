using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<List<IMeal>> Execute()
        {
            if (DateTime.Now.ToString(format) != lastSuccess)
            {
                var meals = await _repository.RetrieveAll();
                lastSuccess = DateTime.Now.ToString(format);
                lastMeals = meals;
            }
            return lastMeals;
        }
    }
}