using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DataLayer.Entities
{
    public class EquipmentDo : AbstractParrentModelDo, IEquipment
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<PersonalEquipmentUserDo> OwningUsers { get; set; }
    }
}
