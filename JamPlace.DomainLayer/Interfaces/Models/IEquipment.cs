﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Interfaces.Models
{
    public interface IEquipment
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        DateTime Date { get; set; }
        int EventId { get; set; }
        IJamUser JamUser { get; set; }
    }
}
