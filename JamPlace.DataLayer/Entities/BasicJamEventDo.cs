using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JamPlace.DataLayer.Entities
{
    public class BasicJamEventDo : AbstractParrentModelDo, IBasicJamEventObject
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
       
    }
}
