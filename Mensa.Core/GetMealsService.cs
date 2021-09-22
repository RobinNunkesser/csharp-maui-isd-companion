using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommonPorts;
using MealPorts;
using Mensa.Core.Ports;

namespace Mensa.Core
{
    public class GetMealsService : IGetMealsService
    {
        private readonly IRepository<int, IMeal> _repository;

        public GetMealsService(IRepository<int, IMeal> repository)
        {
            _repository = repository;
        }

        public async Task Execute(string inDTO, Action<List<IMeal>> successHandler, Action<Exception> errorHandler)
        {
            var result = await _repository.RetrieveAll();
            result.Match(successHandler, errorHandler);
        }
    }
}
