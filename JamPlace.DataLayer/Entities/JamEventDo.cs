using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JamPlace.DataLayer.Entities
{
    internal class JamEventDo : IJamEvent
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public IAdress Adress { get; set; }
        [NotMapped]
        public ICollection<IJamUser> Users { get; set; }
        [NotMapped]
        public ICollection<IEquipment> NeededEquipment { get; set; }
    }
}
