using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DataLayer.Entities
{
    internal class NeededEquipmentEventDo
    {
        int Id { get; set; }
        int JamEventId { get; set; }
        int EquipmentId { get; set; }
        int Quanity { get; set; }
    }
}
