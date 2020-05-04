using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JamPlace.Api.Models
{
    public class SongSearchRequest
    {
        public string SearchText { get; set; }
        public int EventId { get; set; }
    }
}
