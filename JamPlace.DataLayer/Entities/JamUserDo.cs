using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JamPlace.DataLayer.Entities
{
    public class JamUserDo : AbstractParrentModelDo, IJamUser
    {
        public string UserName { get; set; }
        [NotMapped]
        public IEnumerable<IEquipment> PersonalEquipment { get; set; }
        [NotMapped]
        public IEnumerable<PersonalEquipmentUserDo> UserPersonalEquipment { get; set; }
        public string UserIdentityId { get; set; }
        [NotMapped]
        public IEnumerable<IEventEquipment> EventEquipment { get; set ; }
        public ICollection<EventEquipmentDo> EventEquipmentDos { get; set ; }
        public ICollection<JamEventJamUserDo> JamEventJamUser { get; set ; }
        [NotMapped]
        public IEnumerable<IJamEvent> JamEvents { get; set; }
    }
}
