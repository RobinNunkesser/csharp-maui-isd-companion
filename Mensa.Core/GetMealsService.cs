using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Italbytz.Ports.Common;
using Italbytz.Ports.Meal;
using Mensa.Core.Ports;

namespace Mensa.Core
{
    public class GetMealsService : IGetMealsService
    {
        private readonly IDataSource<int, IMeal> _repository;
        private static readonly string format = "yyyy-MM-dd";
        private string lastSuccess = null;
        private List<IMeal> lastMeals;

        public GetMealsService(IDataSource<int, IMeal> repository)
        {
            _repository = repository;
        }

        public async Task Execute(Action<List<IMeal>> successHandler, Action<Exception> errorHandler)
        {
            Result<List<IMeal>> result;
            if (DateTime.Now.ToString(format)!=lastSuccess) { 
                result = await _repository.RetrieveAll();
                result.Match((success) => {
                    lastSuccess = DateTime.Now.ToString(format);
                    lastMeals = success;
                }, (error) => { });
            } else
            {
                result = new Result<List<IMeal>>(lastMeals);
            }
            result.Match(successHandler, errorHandler);
        }
    }
}
