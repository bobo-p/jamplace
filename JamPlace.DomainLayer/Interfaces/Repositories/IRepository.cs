using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JamPlace.DomainLayer.Interfaces.Repositories
{
    public interface IRepository<T> 
    {
        T Add(T item);
        void Delete(T item);
        void Update(T item);
        T Get(int id);
        IEnumerable<T> GetAll();
        
    }
}
