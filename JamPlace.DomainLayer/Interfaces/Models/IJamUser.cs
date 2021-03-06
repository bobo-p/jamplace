﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Interfaces.Models
{
    public interface IJamUser
    {
        int Id { get; set; }
        string UserIdentityId { get; set; }
        string UserName { get; set; }
        IEnumerable<IEquipment> PersonalEquipment { get; set; }
        IEnumerable<IEquipment> EventEquipment { get; set; }
        IEnumerable<IEquipment> NeededEquipment { get; set; }
        IEnumerable<IJamEvent> JamEvents { get; set; }
        ICollection<IComment> Comments { get; set; }
        string PhotoBase64 { get; set; }
    }
}
