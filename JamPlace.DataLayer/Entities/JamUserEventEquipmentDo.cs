using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JamPlace.DataLayer.Entities
{
    [Table("JamUserEventEquipment")]
    public class JamUserEventEquipmentDo : IEquatable<JamUserEventEquipmentDo>
    {
        public int JamUserDoId { get; set; }
        public JamUserDo JamUser { get; set; }
        public int EventEquipmentDoId { get; set; }
        public UserEventEquipmentDo EventEquipment { get; set; }

        public bool Equals(JamUserEventEquipmentDo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(JamUserDoId, other.JamUserDoId) && EventEquipmentDoId == other.EventEquipmentDoId;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((JamUserEventEquipmentDo)obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return (JamUserDoId.GetHashCode() * 397) ^ EventEquipmentDoId;
            }
        }

        public static bool operator ==(JamUserEventEquipmentDo left, JamUserEventEquipmentDo right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(JamUserEventEquipmentDo left, JamUserEventEquipmentDo right)
        {
            return !Equals(left, right);
        }
    }
}
