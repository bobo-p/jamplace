using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Interfaces.Models
{
    public interface IBasicJamEventObject
    {
        int Id { get; set; }
        string Name { get; set; }
        string Size { get; set; }
        string Description { get; set; }
    }
}
