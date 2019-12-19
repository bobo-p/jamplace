using JamPlace.DomainLayer.Interfaces.Models;
using JamPlace.DomainLayer.Interfaces.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace JamPlace.DataLayer.Repositories
{
    class JamEventRepository : IJamEventRepository
    {
        private readonly ApplicationDbContext _context;

        public JamEventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(IJamEvent entity)
        {
            //_context.JamEvents.Add();
            throw new NotImplementedException();

        }

        public void Delete(IJamEvent entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(IJamEvent entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<IJamEvent> GetBy(Expression<Func<IJamEvent, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
