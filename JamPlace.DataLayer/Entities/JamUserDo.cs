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
        public IEnumerable<IEquipment> EventEquipment { get; set ; }
        [NotMapped]
        public IEnumerable<JamUserEventEquipmentDo> UserEventEquipment { get; set ; }
    }
}
