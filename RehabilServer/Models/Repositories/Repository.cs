using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace RehabilServer.Models.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private RehabilEntities entities;
        private readonly DbSet<T> dbSet;

        public Repository()
        {
            this.entities = AppDbContext.DB;
            dbSet = entities.Set<T>();
        }

        #region Implementation

        public int Add(T entity)
        {
            try
            {
                dbSet.Add(entity);
            }
            catch (EntityException)
            {
                return 0;
            }
            return 1;
        }

        public int Update(T entity)
        {
            try
            {
                dbSet.Attach(entity);
                entities.Entry(entity).State = EntityState.Modified;
            }
            catch (EntityException)
            {
                return 0;
            }
            return 1;
        }

        public int Delete(T entity)
        {
            try
            {
                dbSet.Remove(entity);
            }
            catch (EntityException)
            {
                return 0;
            }
            return 1;
        }

        public int Delete(Expression<Func<T, bool>> where)
        {
            try
            {
                IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();

                foreach (T obj in objects)
                    dbSet.Remove(obj);
            }
            catch (EntityException)
            {
                return 0;
            }
            return 1;
        }

        public T FindById(Guid id, params string[] navigations)
        {
            try
            {
                IQueryable<T> set = dbSet.AsQueryable();

                foreach (string nav in navigations)
                    set = set.Include(nav);

                return set.FirstOrDefault(f => f.Equals(id));
            }
            catch (EntityException)
            {
                return null;
            }
        }

        public IEnumerable<T> GetAll(params string[] navigations)
        {
            try
            {
                var set = dbSet.AsQueryable();

                foreach (string nav in navigations)
                    set = set.Include(nav);

                return set.AsEnumerable();
            }
            catch (EntityException)
            {
                return null;
            }
        }

        public IEnumerable<T> GetIf(Expression<Func<T, bool>> where, params string[] navigations)
        {
            try
            {
                var set = dbSet.AsQueryable();

                foreach (string nav in navigations)
                    set = set.Include(nav);

                return set.Where(where).AsEnumerable();
            }
            catch (EntityException)
            {
                return null;
            }
        }

        public T Get(Expression<Func<T, bool>> where, params string[] navigations)
        {
            try
            {
                var set = dbSet.AsQueryable();

                foreach (string nav in navigations)
                    set = set.Include(nav);

                return set.Where(where).FirstOrDefault<T>();
            }
            catch (EntityException)
            {
                return null;
            }
        }
        #endregion 


    }
}