using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Models
{
    public class JamUser : IJamUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public ICollection<IEquipment> Equipment { get; set; }
    }
}
