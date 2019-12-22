using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace JamPlace.DataLayer.Entities
{
    [Table("PersonalEquipmentUser")]
    public class PersonalEquipmentUserDo: IEquatable<PersonalEquipmentUserDo>
    {
        public int JamUserDoId { get; set; }
        public JamUserDo JamUser { get; set; }
        public int EquipmentDoId { get; set; }
        public EquipmentDo Equipment { get; set; }

        public bool Equals(PersonalEquipmentUserDo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(JamUserDoId, other.JamUserDoId) && EquipmentDoId == other.EquipmentDoId;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PersonalEquipmentUserDo)obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return (JamUserDoId.GetHashCode() * 397) ^ EquipmentDoId;
            }
        }

        public static bool operator ==(PersonalEquipmentUserDo left, PersonalEquipmentUserDo right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PersonalEquipmentUserDo left, PersonalEquipmentUserDo right)
        {
            return !Equals(left, right);
        }
    }
}
