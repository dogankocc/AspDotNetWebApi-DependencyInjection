using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace RehabilServer.Models.Repositories
{
    public interface IRepository<T>   where T : class
    {
        int Add(T entity);

        int Update(T entity);

        int Delete(T entity);

        int Delete(Expression<Func<T, bool>> where);

        T FindById(Guid id, params string[] navigations);

        T Get(Expression<Func<T, bool>> where, params string[] navigations);

        IEnumerable<T> GetAll(params string[] navigations);

        IEnumerable<T> GetIf(Expression<Func<T, bool>> where, params string[] navigations);
    }
}