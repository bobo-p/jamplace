using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Interfaces.Models
{
    public interface INeededEquipmentEvent
    {
        int Id { get; set; }
        int JamEventId { get; set; }
        int EquipmentId { get; set; }
        int Quanity { get; set; }
    }
}
