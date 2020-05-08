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
        UserAccessModeEnum GetAccesTypeForUser(int eventId, string userId);
        IEnumerable<IJamEvent> GetFiltereByUser(string userId);
        IEnumerable<IJamEvent> GetFiltereByNameForUser(string seacrhText, string userId);
        IEnumerable<IJamEvent> GetAllNotJoined(string userId);
        void Join(int eventId, string userId);
        void Delete(int id);
        void LeaveEvent(int eventId, string userId);
    }
}
