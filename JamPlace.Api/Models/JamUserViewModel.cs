﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JamPlace.Api.Models
{
    public class JamUserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PhotoBase64 { get; set; }
    }
}
