﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JamPlace.Api.Models
{
    public class AddressViewModel
    {
        public string Street { get; set; }
        public string LocalNumber {get; set;}
        public string City {get; set;}
        public string Country {get; set;}
    }
}
