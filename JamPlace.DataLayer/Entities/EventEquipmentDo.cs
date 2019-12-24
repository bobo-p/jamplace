using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace JamPlace.DataLayer.Entities
{
    public class EventEquipmentDo : AbstractParrentModelDo, IEventEquipment, IEquatable<EventEquipmentDo>
    {
        [ForeignKey("JamUserDo")]
        public int JamUserId { get; set; }
        public JamUserDo JamUser { get; set; }
        [ForeignKey("JamEventDo")]
        public int JamEventId { get; set; }
        public JamEventDo JamEvent { get; set; }
        [NotMapped]
        public IEnumerable<IEquipment> Equpiment { get; set; }
        public ICollection<EquipmentEventEquipmentDo> UserEventEventEqupiment { get; set; }

        public bool Equals(EventEquipmentDo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Id, other.Id);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EventEquipmentDo)obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return (Id.GetHashCode() * 397);
            }
        }

        public static bool operator ==(EventEquipmentDo left, EventEquipmentDo right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EventEquipmentDo left, EventEquipmentDo right)
        {
            return !Equals(left, right);
        }
    }    
}
