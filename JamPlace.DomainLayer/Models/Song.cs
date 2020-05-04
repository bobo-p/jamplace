using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Models
{
    public class Song : ISong
    {
        public int Id { get; set; }
        public string Title { get;set;}
        public string Artist { get;set;}
        public string Description { get;set;}
        public string Link { get;set;}
        public IJamEvent JamEvent { get; set; }
        public DateTime AddDate { get; set; }
    }
}
