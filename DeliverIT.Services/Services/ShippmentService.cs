using DeliverIT.Models;
using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeliverIT.Services.Services
{
    public class ShippmentService : IShippmentService
    {
        private readonly DeliverITDBContext db;

        public ShippmentService(DeliverITDBContext db)
        {
            this.db = db;
        }
        public Task<Shipment> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Shipment>> Get()
        {
            throw new NotImplementedException();

        }

        public Task<Shipment> Post(Shipment obj)
        {
            throw new NotImplementedException();
        }

        public Task<Shipment> Update(int id, Shipment obj)
        {
            throw new NotImplementedException();
        }
    }
}
