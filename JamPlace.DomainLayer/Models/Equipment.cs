﻿using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Models
{
    public class Equipment : IEquipment
    {
        public int Id { get; set; }
        public string Name { get;set;}
        public string Description { get;set;}
    }
}