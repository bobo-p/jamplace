using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Interfaces.Models
{
    public interface IUserEventEquipment
    {
        int JamUserId { get; set; }
        int EquipmentId { get; set; }
        int JamEventId { get; set; }
        IEnumerable<IEquipment> EventEqupiments { get; set; }
        IEnumerable<IJamUser> JamUsers { get; set; }
    }
}
