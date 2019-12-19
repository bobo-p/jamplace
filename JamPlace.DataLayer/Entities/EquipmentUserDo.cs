using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DataLayer.Entities
{
    internal class EquipmentUserDo
    {
        int Id { get; set; }
        int JamUserId { get; set; }
        int EquipmentId { get; set; }
        int JamEventId { get; set; }
    }
}
