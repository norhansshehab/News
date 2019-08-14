using Microsoft.EntityFrameworkCore;
using MyApi.MyContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyApi.Repository
{
    class GenericRepository<T> : IRepository<T> where T : class
    {
        private MyDbContext _db;

        public GenericRepository(MyDbContext db)
        {
            _db = db;
        }

        public T Create(T obj)
        {
            _db.Set<T>().Add(obj);
            _db.SaveChanges();

            return obj;
        }

        public bool Delete(Object id)
        {
            _db.Set<T>().Remove(_db.Set<T>().Find(id));
            _db.SaveChanges();
           return true;
        }

        public T Edit(T obj)
        {
            _db.Set<T>().Attach(obj);
            _db.Entry(obj).State = EntityState.Modified;
            _db.SaveChanges();
            return obj;
        }

        public IEnumerable<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public T GetById(Object id)
        {
            return _db.Set<T>().Find(id);
        }


       
    }
}
