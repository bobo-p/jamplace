using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Interfaces.Services
{
    public interface IService<T>
    {        
        void Add(T item);
        void Delete(T item);
        void Edit(T item);
        T Get(int id);
    }
}
