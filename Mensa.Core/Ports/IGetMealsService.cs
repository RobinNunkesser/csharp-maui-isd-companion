using System;
using System.Collections.Generic;
using Italbytz.Ports.Common;
using Italbytz.Ports.Meal;

namespace Mensa.Core.Ports
{
    public interface IGetMealsService : IService<List<IMealCollection>>
    {
    }
}
