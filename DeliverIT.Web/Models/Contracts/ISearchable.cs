using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Web.Models.Contracts
{
    public interface ISearchable
    {
        string FilterTag { get; set; }
    }
}
