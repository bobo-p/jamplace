using JamPlace.DomainLayer.Interfaces.Models;
using JamPlace.DomainLayer.Interfaces.Repositories;
using JamPlace.DomainLayer.Interfaces.Services;
using JamPlace.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Services
{
    public class JamUserService : IJamUserService
    {
        private readonly IJamUserRepository _jamUserRepository;
        public JamUserService(IJamUserRepository jamUserRepository)
        {
            _jamUserRepository = jamUserRepository;
        }
        public void Add(IJamUser item)
        {
            _jamUserRepository.Add(item);
        }

        public void Delete(IJamUser item)
        {
            _jamUserRepository.Delete(item);
        }

        public void Edit(IJamUser item)
        {
            _jamUserRepository.Update(item);
        }

        public IJamUser Get(int id)
        {
            return _jamUserRepository.Get(id);
        }

        public IJamUser GetByIdentityId(string Id)
        {
            var user = _jamUserRepository.GetByIdentityId(Id);
            if (user != null)
                return user;
            user = new JamUser() { UserIdentityId = Id };
            var userId = _jamUserRepository.Add(user);
            user.Id = userId;
            return user;
        }
    }
}
