using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Models
{
    public class JamEvent : IJamEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public IAdress Adress { get; set; }
        public ICollection<IJamUser> Users { get; set; }
        public ICollection<IEquipment> NeededEquipment { get; set; }
        public DateTime Date { get; set; }
    }
}
