using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<T>
    {
        Task<IList<T>> GetAll();
        Task<T> GetById(string id);

        Task<T> Create(T obj);
        Task<bool> Delete(string id);
        Task<T> Edit(T obj);

      
    }
}
