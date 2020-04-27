using JamPlace.DomainLayer.Common;
using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Interfaces.Services
{
    public interface IJamEventService : IService<IJamEvent>
    {
        void  Add(IJamEvent jamEvent, IJamUser eventUser);
        IEnumerable<IJamEvent> GetFilteredPageByUserId(int pageIndex, int pageSize, bool orderByDate, string city,int userId);
        IEnumerable<IJamEvent> GetFilteredPage(int pageIndex, int pageSize, bool orderByDate, string city);
        AccessModeEnum GetAccesTypeForUser(int eventId, string userId);
    }
}
