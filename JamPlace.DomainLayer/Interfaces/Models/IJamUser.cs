using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Interfaces.Models
{
    public interface IJamUser
    {
        string Id { get; set; }
        string UserName { get; set; }
        ICollection<IEquipment> Equipment { get; set; }
    }
}
