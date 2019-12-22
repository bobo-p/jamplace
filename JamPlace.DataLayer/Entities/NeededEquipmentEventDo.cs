using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DataLayer.Entities
{
    public class NeededEquipmentEventDo: AbstractParrentModelDo, INeededEquipmentEvent
    {
        public int JamEventId { get; set; }
        public int EquipmentId { get; set; }
        public int Quanity { get; set; }
    }
}
