﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Interfaces.Models
{
    public interface IComment
    {
        int Id { get; set; }
        string Content { get; set; }
        DateTime Date { get; set; }
        int EventId { get; set; }
        IJamUser JamUser { get; set; }
    }
}
