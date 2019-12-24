using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DataLayer.Entities
{
    public class JamEventJamUserDo
    {
        public int JamUserDoId { get; set; }
        public JamUserDo JamUser { get; set; }
        public int JamEventDoId { get; set; }
        public JamEventDo JamEvent { get; set; }
    }
}
