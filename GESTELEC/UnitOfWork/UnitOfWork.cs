using GESTELEC.Interfaces;
using GESTELEC.Models;
using GESTELEC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace GESTELEC.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private GestelecContext db;
        private IGenericRepository<T> _entity;

        public UnitOfWork()
        {
            this.db = new GestelecContext();
        }

        public UnitOfWork(GestelecContext db)
        {
            this.db = db;
        }

        public IGenericRepository<T> Entity
        {
            get { return _entity ?? (_entity = new GenericRepository<T>(db)); }
        }

        public void Save()
        {
            db.SaveChanges();
        }
        public void Dispose()
        {
            db.Dispose();
        }


    }
}
