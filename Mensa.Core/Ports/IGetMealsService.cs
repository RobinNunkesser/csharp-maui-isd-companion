using System;
using System.Collections.Generic;
using CommonPorts;
using MealPorts;

namespace Mensa.Core.Ports
{
    public interface IGetMealsService : IService<string,List<IMeal>>
    {
    }
}
