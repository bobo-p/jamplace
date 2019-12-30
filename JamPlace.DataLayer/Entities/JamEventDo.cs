using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JamPlace.DataLayer.Entities
{
    public class JamEventDo : AbstractParrentModelDo, IJamEvent
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        [ForeignKey("AdressDo")]
        public int AddressId { get; set; }
        public AdressDo EventAdress { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public IEnumerable<EventEquipmentDo> EventEquipmentDos { get; set; }
        [NotMapped]
        public ICollection<JamEventJamUserDo> JamEventJamUser { get; set; }
        [NotMapped]
        public ICollection<NeededEquipmentEventDo> NeededEventEquipment { get; set; }
        [NotMapped]
        public IAdress Adress { get; set ; }
        [NotMapped]
        public ICollection<IJamUser> Users { get ; set ; }
        [NotMapped]
        public ICollection<IEquipment> NeededEquipment { get; set; }
    }
}
