using JamPlace.DataLayer.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DataLayer.Entities
{
    internal class JamEventJamUserDo
    {
        public int JamUserDoId { get; set; }
        public JamUserDo JamUser { get; set; }
        public int JamEventDoId { get; set; }
        public JamEventDo JamEvent { get; set; }
        public AccessModeEnum AccessMode { get; set; }
    }
}
