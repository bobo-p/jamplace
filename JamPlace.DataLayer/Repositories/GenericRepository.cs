using JamPlace.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamPlace.DataLayer.Repositories
{
    public abstract class GenericRepository<T,C>  where C :  AbstractParrentModelDo, T
    {
        protected readonly ApplicationDbContext Context;
        public GenericRepository(ApplicationDbContext context)
        {
            Context = context;
        }
        public int Add(T item)
        {
            var ent = item as C;
            var data = Context.Add(ent);
            Context.SaveChanges();
            Context.Entry(item).State = EntityState.Detached;
            return data.Entity.Id;
        }

        public void  Delete(T item)
        {            
            Context.Remove(item as C);
            Context.SaveChanges();
            Context.Entry(item).State = EntityState.Detached;
        }

        public T Get(int id)
        {
            var item = Context.Set<C>().FirstOrDefault(p => p.Id == id);
            return item;
        }

        public IEnumerable<T> GetAll()
        {
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var type = typeof(T).GetType();
            return Context.Set<C>().Select(p => (T)p);
        }

        public void AddMany(IEnumerable<T> items)
        {
            Context.AddRange(items);
            Context.SaveChanges();
        }

        public IEnumerable<T> GetByCondition(Func<T, bool> function)
        {
            return Context.Set<C>().Where(function);
        }

        public T GetFirstOrDefaultByCondition(Func<T, bool> function)
        {
            return Context.Set<C>().FirstOrDefault(function);
        }

        public void Update(T item)
        {
            Context.Update(item);
            Context.SaveChanges();
            Context.Entry(item).State = EntityState.Detached;
        }
    }
}
