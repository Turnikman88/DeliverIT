using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverIT.Services.Contracts
{
    
    public interface ICRUDshared<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T> PostAsync(T obj);
        Task<T> UpdateAsync(int id, T obj);
        Task<T> DeleteAsync(int id);
    }
}
