using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.DTOs;
using System.Collections.Generic;

namespace DeliverIT.Services.Contracts
{
    public interface IWareHouseService
    {
        IEnumerable<WareHouseDTO> Locations();
    }
}
