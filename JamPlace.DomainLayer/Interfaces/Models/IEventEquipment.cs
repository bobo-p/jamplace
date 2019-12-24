using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Interfaces.Models
{
    public interface IEventEquipment
    {
        int Id { get; set; }
        int JamUserId { get; set; }
        int JamEventId { get; set; }
        IEnumerable<IEquipment> Equpiment { get; set; }
    }
}
