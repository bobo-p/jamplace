using JamPlace.DomainLayer.Interfaces.Models;
using JamPlace.DomainLayer.Interfaces.Repositories;
using JamPlace.DomainLayer.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamPlace.DomainLayer.Services
{
    public class NeededEventEquipmentService : INeededEventEquipmentService
    {
        private readonly INeededEquipmentRepository _equipmenttRepository;
        private readonly IJamEventRepository _jamEventRepository;
        private readonly IJamUserRepository _jamUserRepository;

        public NeededEventEquipmentService(INeededEquipmentRepository eqRepository, IJamEventRepository jamEventRepository, IJamUserRepository jamUserRepository)
        {
            _equipmenttRepository = eqRepository;
            _jamEventRepository = jamEventRepository;
            _jamUserRepository = jamUserRepository;
        }

        public IEquipment Add(IEquipment equipment, string userIdentityId)
        {
            var user = _jamUserRepository.GetByIdentityId(userIdentityId);
            equipment.JamUser = user;
            equipment.Date = DateTime.Now;
            var addedUser = _equipmenttRepository.Add(equipment);
            addedUser.JamUser = user;
            return addedUser;
        }

        public IEquipment Add(IEquipment item)
        {
            throw new NotImplementedException();
        }

        public void Delete(IEquipment item)
        {
            _equipmenttRepository.Delete(item);
        }

        public void Edit(IEquipment comment, int eventId)
        {
            var jamEvent = _jamEventRepository.Get(eventId);
            //TODO check if user has acces
            _equipmenttRepository.Update(comment);
        }

        public void Edit(IEquipment item)
        {
            throw new NotImplementedException();
        }

        public IEquipment Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEquipment> GetFilteredByName(string seacrhText, int eventId)
        {
            var eqs = _equipmenttRepository.GetFilteredByEvent(eventId).ToList();
            if (string.IsNullOrEmpty(seacrhText)) return eqs.OrderByDescending(p => p.Date);
            return eqs.Where(ev => !string.IsNullOrEmpty(ev.JamUser.UserName) && ev.JamUser.UserName.ToLower().Contains(seacrhText.ToLower())).OrderByDescending(p => p.Date);
        }
    }
}
