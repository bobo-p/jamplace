using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JamPlace.DataLayer.Entities
{
    public class AdressDo : AbstractParrentModelDo, IAdress
    {
        public string Street { get; set; }
        public string LocalNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        [NotMapped]
        public IEnumerable<IJamEvent> Jams { get; set; }
        public ICollection<JamEventDo> JamEvents { get; set; }
    }
}
