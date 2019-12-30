using JamPlace.DomainLayer.Interfaces.Models;
using JamPlace.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Interfaces.Repositories
{
    public interface IJamUserRepository : IRepository<IJamUser>
    {
        IJamUser GetWithEventEq(int id, int eventId);
        IEnumerable<IJamEvent> GetEventsFilteredPageByUserId(int pageIndex, int pageSize, bool orderByDate, string city, int userId);
    }
}
