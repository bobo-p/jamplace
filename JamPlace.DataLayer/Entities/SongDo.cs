using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DataLayer.Entities
{
    public class SongDo : AbstractParrentModelDo, ISong
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }
}
