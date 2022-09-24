using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaNmbrs.DistributedCacheSample.Entities;

namespace VismaNmbrs.DistributedCacheSample.Data
{
    public interface IAsyncDatabase<T> where T : BaseEntity
    {
        Task<T> Add(T itemToAdd);
        Task<IList<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<T> Update(T itemToUpdate);
        Task<T> Delete(Guid id);
    }
}
