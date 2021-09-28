using DeliverIT.Models.DatabaseModels;
using System.Collections.Generic;

namespace DeliverIT.Services.Contracts
{
    public interface IWareHouseService
    {
        IEnumerable<WareHouse> Locations();
    }
}
