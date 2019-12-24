using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Interfaces.Models
{
    public interface IAdress
    {
        int Id { get; set; }
        string Street { get; set; }
        string LocalNumber { get; set; }
        string City { get; set; }
        string Country { get; set; }
        IEnumerable<IJamEvent> Jams{get;set;}
    }
}
