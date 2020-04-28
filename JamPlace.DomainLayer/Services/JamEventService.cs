using JamPlace.DomainLayer.Common;
using JamPlace.DomainLayer.Interfaces.Models;
using JamPlace.DomainLayer.Interfaces.Repositories;
using JamPlace.DomainLayer.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Services
{
    public class JamEventService : IJamEventService
    {
        private readonly IJamEventRepository _jamEventRepository;
        private readonly IJamUserRepository _jamUserRepository;

        public JamEventService(IJamEventRepository jamEventRepository, IJamUserRepository jamUserRepository)
        {
            _jamEventRepository = jamEventRepository;
            _jamUserRepository = jamUserRepository;
        }

        public IJamEvent Add(IJamEvent item)
        {
           return  _jamEventRepository.Add(item);
        }

        public void Add(IJamEvent jamEvent, IJamUser eventUser)
        {
            throw new NotImplementedException();
        }

        public void Delete(IJamEvent item)
        {
            _jamEventRepository.Delete(item);
        }

        public void Edit(IJamEvent item)
        {
            _jamEventRepository.Update(item);
        }

        public IJamEvent Get(int id)
        {
            return _jamEventRepository.Get(id);
        }

        public UserAccessModeEnum GetAccesTypeForUser(int eventId, string userId)
        {
            return _jamEventRepository.GetAccesTypeForUser(eventId,userId);
        }

        public IEnumerable<IJamEvent> GetFilteredPage(int pageIndex, int pageSize, bool orderByDate, string city)
        {
            return _jamEventRepository.GetFilteredPage(pageIndex,pageSize,orderByDate,city);
        }

        public IEnumerable<IJamEvent> GetFilteredPageByUserId(int pageIndex, int pageSize, bool orderByDate, string city, int userId)
        {
            return _jamUserRepository.GetEventsFilteredPageByUserId(pageIndex, pageSize, orderByDate, city,userId);
        }
    }
}
