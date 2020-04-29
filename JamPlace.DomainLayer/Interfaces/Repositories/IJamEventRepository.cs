using JamPlace.DomainLayer.Common;
using JamPlace.DomainLayer.Interfaces.Models;
using JamPlace.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Interfaces.Repositories
{
    public interface IJamEventRepository : IRepository<IJamEvent>
    {
        IEnumerable<IJamEvent> GetFilteredPage(int pageIndex, int pageSize, bool orderByDate, string city);
        UserAccessModeEnum GetAccesTypeForUser(int eventId, string userId);
        IEnumerable<IJamEvent> GetFiltereByUser(string userId);
    }
}
