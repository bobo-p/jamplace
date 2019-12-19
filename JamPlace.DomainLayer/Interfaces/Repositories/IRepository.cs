using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace JamPlace.DomainLayer.Interfaces.Repositories
{
    public interface IRepository<T> 
    {
        IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
    }
}
