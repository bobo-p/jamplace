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
        public ICollection<CommentDo> CommentsDo { get; set; }
        public ICollection<EquipmentDo> ProvidedEquipmentDos { get; set; }
        public ICollection<NeededEquipmentDo> NeededEquipmentDos { get; set; }
        [NotMapped]
        public IEnumerable<IEquipment> PersonalEquipment { get; set; }
        public string UserIdentityId { get; set; }
        [NotMapped]
        public IEnumerable<IEquipment> EventEquipment { get; set ; }
        [NotMapped]
        public IEnumerable<IEquipment> NeededEquipment { get; set; }
        internal ICollection<JamEventJamUserDo> JamEventJamUser { get; set ; }
        [NotMapped]
        public IEnumerable<IJamEvent> JamEvents { get; set; }
        [NotMapped]
        public ICollection<IComment> Comments { get; set; }
        public string PhotoBase64 { get; set; }
    }
}
