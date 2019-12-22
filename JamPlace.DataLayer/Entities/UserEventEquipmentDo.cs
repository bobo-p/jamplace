using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JamPlace.DataLayer.Entities
{
    public class UserEventEquipmentDo : AbstractParrentModelDo, IUserEventEquipment
    {
        public int JamUserId { get; set; }
        public int EquipmentId { get; set; }
        public int JamEventId { get; set; }
        [NotMapped]
        public ICollection<JamUserEventEquipmentDo> EventJamUsers { get; set; }
        [NotMapped]
        public IEnumerable<IEquipment> EventEqupiments { get; set; }
        [NotMapped]
        public IEnumerable<IJamUser> JamUsers { get; set; }
    }    
}
