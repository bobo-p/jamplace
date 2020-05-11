using JamPlace.DomainLayer.Common;
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
        public EventAccessTypeEnum AccessType { get; set; }
        public ICollection<SongDo> SongsDo { get; set; }
        public ICollection<CommentDo> CommentsDo { get; set; }
        public  ICollection<EquipmentDo> ProvidedEquipmentDos { get; set; }
        public ICollection<NeededEquipmentDo> NeededEquipmentDos { get; set; }
        [NotMapped]
        public IAdress Address { get; set; }
        [NotMapped]
        public ICollection<IJamUser> Users { get; set; }
        [NotMapped]
        public ICollection<ISong> Songs { get; set; }
        [NotMapped]
        public ICollection<IEquipment> NeededEquipment { get; set; }
        [NotMapped]
        public ICollection<IComment> Comments { get; set; }
        [NotMapped]
        public IJamUser Creator { get; set; }
        [NotMapped]
        public ICollection<IEquipment> ProvidedEquipment { get; set; }
        internal ICollection<JamEventJamUserDo> JamEventJamUser { get; set; }
    }
}
