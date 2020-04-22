using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JamPlace.Api.Models
{
    public class SongViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }
}
