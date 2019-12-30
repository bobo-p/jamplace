using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Interfaces.Services
{
    public interface IJamEventService : IService<IJamEvent>
    {
        IEnumerable<IJamEvent> GetFilteredPageByUserId(int pageIndex, int pageSize, bool orderByDate, string city,int userId);
        IEnumerable<IJamEvent> GetFilteredPage(int pageIndex, int pageSize, bool orderByDate, string city);
    }
}
