using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Models
{
    public class Adress : IAdress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string LocalNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
