using JamPlace.DomainLayer.Interfaces.Models;
using JamPlace.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Interfaces.Repositories
{
    public interface ISongRepository : IRepository<ISong>
    {
        IEnumerable<ISong> GetFilteredByEvent(int eventId);
    }
}
