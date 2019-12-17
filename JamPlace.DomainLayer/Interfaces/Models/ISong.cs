using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Interfaces.Models
{
    public interface ISong
    {
        int Id { get; set; }
        string Title { get; set; }
        string Artist { get; set; }
        string Description { get; set; }
        string Link { get; set; }
    }
}
