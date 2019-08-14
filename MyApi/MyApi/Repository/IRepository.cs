using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(Object id);

        T Create(T obj);
        bool Delete(Object id);
        T Edit(T obj);

       
    }
}
