using JamPlace.DomainLayer.Common;
using JamPlace.DomainLayer.Interfaces.Models;
using JamPlace.DomainLayer.Interfaces.Repositories;
using JamPlace.DomainLayer.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public void Join(int eventId, string userId)
        {
            var jamEvent = _jamEventRepository.Get(eventId);
            var userExsist = jamEvent.Users.FirstOrDefault(p => p.UserIdentityId == userId);
            if(userExsist ==null)
            {
                var user = _jamUserRepository.GetByIdentityId(userId);
                _jamEventRepository.GrantGuestAccessUser(jamEvent.Id, user.Id);
            }
        }
        
        public void Delete(IJamEvent item)
        {
            _jamEventRepository.Delete(item);
        }

        public void Edit(IJamEvent item)
        {
            var currentEvent = _jamEventRepository.Get(item.Id);
            if(item.Address!=null)
            {
                if (currentEvent.Address == null)
                    currentEvent.Address = item.Address;
                else
                {
                    currentEvent.Address.Street = item.Address.Street;
                    currentEvent.Address.LocalNumber = item.Address.LocalNumber;
                    currentEvent.Address.City = item.Address.City;
                    currentEvent.Address.Country = item.Address.Country;
                }
            }
            currentEvent.Name = item.Name;
            currentEvent.Description = item.Description;
            currentEvent.Date = item.Date;
            currentEvent.Size = item.Size;
            currentEvent.AccessType = item.AccessType;

            _jamEventRepository.SimpleUpdate(currentEvent);
        }

        public IJamEvent Get(int id)
        {
            return _jamEventRepository.Get(id);
        }

        public UserAccessModeEnum GetAccesTypeForUser(int eventId, string userId)
        {
            return _jamEventRepository.GetAccesTypeForUser(eventId,userId);
        }

        public IEnumerable<IJamEvent> GetAllNotJoined(string userId)
        {
            var jams = _jamEventRepository.GetAll();
            var output = jams.Where(p => p?.Users.FirstOrDefault(u => u.UserIdentityId == userId) == null);
            return output;
        }

        public IEnumerable<IJamEvent> GetFiltereByNameForUser(string seacrhText, string userId)
        {
            var events =_jamEventRepository.GetFiltereByUser(userId).ToList();
            if (string.IsNullOrEmpty(seacrhText)) return events;
            return events.Where(ev => ev.Name.ToLower().Contains(seacrhText.ToLower()));
        }

        public IEnumerable<IJamEvent> GetFiltereByUser(string userId)
        {
            return _jamEventRepository.GetFiltereByUser(userId);
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
