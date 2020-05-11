using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Interfaces.Services
{
    public interface INeededEventEquipmentService : IService<IEquipment>
    {
        IEquipment Add(IEquipment comment, string userIdentityId);
        void Edit(IEquipment comment, int eventId);
        IEnumerable<IEquipment> GetFilteredByName(string seacrhText, int eventId);
    }
}
