using JamPlace.DomainLayer.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Interfaces.Models
{
    public interface IJamEvent
    {
        int Id { get; set; }
        string Name { get; set; }
        string Size { get; set; }
        string Description { get; set; }
        IAdress Address { get; set; }
        ICollection<IJamUser> Users {get;set;}
        ICollection<ISong> Songs { get; set; }
        ICollection<IEquipment> NeededEquipment {get;set;}
        DateTime Date { get; set; }
        EventAccessTypeEnum AccessType { get; set; }
        ICollection<IComment> Comments { get; set; }

    }
}
