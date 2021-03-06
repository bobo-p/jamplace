﻿using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Models
{
    public class Comment : IComment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserId { get;set;}
        public DateTime Date { get; set; }
        public int EventId { get; set; }
        public IJamUser JamUser { get; set; }
    }
}
