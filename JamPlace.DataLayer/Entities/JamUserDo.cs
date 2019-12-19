using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JamPlace.DataLayer.Entities
{
    internal class JamUserDo : IJamUser
    {
        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }
        [NotMapped]
        public ICollection<IEquipment> Equipment { get; set; }
    }
}
