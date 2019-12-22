using JamPlace.DataLayer.Entities;
using JamPlace.DomainLayer.Interfaces.Models;
using JamPlace.DomainLayer.Interfaces.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace JamPlace.DataLayer.Repositories
{
    public class BasicJamEventRepository : GenericRepository<IBasicJamEventObject,BasicJamEventDo>, IBasicJamEventRepository
    {
        
        public BasicJamEventRepository(ApplicationDbContext context): base(context)
        {
            
        }      
        

       
    }
}
