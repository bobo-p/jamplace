using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Models
{
    public class JamUser : IJamUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public IEnumerable<IEquipment> PersonalEquipment { get; set; }
        public string UserIdentityId { get; set; }
        public IEnumerable<IEventEquipment> EventEquipment { get; set; }
        public IEnumerable<IJamEvent> JamEvents { get; set; }
        public ICollection<IComment> Comments { get; set; }
    }
}
