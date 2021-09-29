using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverIT.Services.Contracts
{
    
    public interface ICRUDshared<T> where T : class
    {
        Task<IEnumerable<T>> Get();
        Task<T> Post(T obj);
        Task<T> Update(int id, T obj);
        Task<T> Delete(int id);
    }
}
