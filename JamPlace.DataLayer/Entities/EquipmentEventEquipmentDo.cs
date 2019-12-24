using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JamPlace.DataLayer.Entities
{
    [Table("EquipmentEventEquipment")]
    public class EquipmentEventEquipmentDo : IEquatable<EquipmentEventEquipmentDo>
    {
        public int EquipmentDoId { get; set; }
        public EquipmentDo Equipment { get; set; }
        public int EventEquipmentDoId { get; set; }
        public EventEquipmentDo EventEquipment { get; set; }

        public bool Equals(EquipmentEventEquipmentDo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(EventEquipmentDoId, other.EventEquipmentDoId) && EquipmentDoId == other.EquipmentDoId;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EquipmentEventEquipmentDo)obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return (EventEquipmentDoId.GetHashCode() * 397) ^ EquipmentDoId;
            }
        }

        public static bool operator ==(EquipmentEventEquipmentDo left, EquipmentEventEquipmentDo right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EquipmentEventEquipmentDo left, EquipmentEventEquipmentDo right)
        {
            return !Equals(left, right);
        }
    }
}
