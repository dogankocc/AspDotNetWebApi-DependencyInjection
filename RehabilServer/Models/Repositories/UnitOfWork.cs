using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RehabilServer.Models.Repositories
{
    public class UnitOfWork : IUnitOfWork 
    {
        private RehabilEntities entity;

        public UnitOfWork()
        {
            entity = AppDbContext.DB;
        }

        public void Dispose()
        {
            entity.Dispose();
        }

        public void SaveChange()
        {
            entity.SaveChanges();
        }
    }
}