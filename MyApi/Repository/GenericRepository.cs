using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    class GenericRepository<T> : IRepository<T> where T : class
    {
        public Task<T> Create(T obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<T> Edit(T obj)
        {
            throw new NotImplementedException();
        }

        public Task<IList<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
