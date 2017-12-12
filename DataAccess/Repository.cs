using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Repository<T>
        where T : class
    {
        private DataBaseContext _context;

        public Repository()
        {
            _context = new DataBaseContext();
        }

        public void Add(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Added;
        }

        public void Remove(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Deleted;
        }

        public T Update(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Modified;

            return entity;
        }

        public IEnumerable<T> Read()
        {
            return _context.Set<T>().ToList();
        }

        public IQueryable<T> Set()
        {
            return _context.Set<T>();
        }

        public T GetEmployeeById(int id) 
        {
            return _context.Set<T>().Find(id);
        }

        public T GetCountryByName(string name)
        {
            return _context.Set<T>().Find(name);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
