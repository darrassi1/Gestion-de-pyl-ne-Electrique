using GESTELEC.Interfaces;
using GESTELEC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GESTELEC.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private GestelecContext db;
        private DbSet<T> table = null;

        public GenericRepository()
        {
            this.db = new GestelecContext();
            table = db.Set<T>();
        }

        public GenericRepository(GestelecContext db)
        {
            this.db = db;
            table = db.Set<T>();
        }

        public void Delete(object Id)
        {
            T existing = GetById(Id);
            table.Remove(existing);
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object Id)
        {
            

            return table.Find(Id);
        }

        public void Insert(T entity)
        {
            table.Add(entity);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(T entity)
        {
            table.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
        }
    }
}