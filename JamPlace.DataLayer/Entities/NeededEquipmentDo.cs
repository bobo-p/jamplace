using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JamPlace.DataLayer.Entities
{
    public class NeededEquipmentDo : AbstractParrentModelDo, IEquipment
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("Id")]
        public int EventId { get; set; }
        public JamEventDo Event { get; set; }
        [ForeignKey("Id")]
        public int UserId { get; set; }
        public JamUserDo User { get; set; }
        [NotMapped]
        public IJamUser JamUser { get; set; }
        public DateTime Date { get; set; }
    }
}
