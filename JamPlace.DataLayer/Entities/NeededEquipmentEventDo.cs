using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JamPlace.DataLayer.Entities
{
    [Table("NeededEquipmentEventDo")]
    public class NeededEquipmentEventDo : AbstractParrentModelDo, IEquatable<NeededEquipmentEventDo>
    {
        public int JamEventDoId { get; set; }
        public JamEventDo JamEvent { get; set; }
        public int EquipmentDoId { get; set; }
        public EquipmentDo Equipment { get; set; }

        public bool Equals(NeededEquipmentEventDo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(JamEventDoId, other.JamEventDoId) && EquipmentDoId == other.EquipmentDoId;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((NeededEquipmentEventDo)obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return (JamEventDoId.GetHashCode() * 397) ^ EquipmentDoId;
            }
        }

        public static bool operator ==(NeededEquipmentEventDo left, NeededEquipmentEventDo right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(NeededEquipmentEventDo left, NeededEquipmentEventDo right)
        {
            return !Equals(left, right);
        }
    }
}
