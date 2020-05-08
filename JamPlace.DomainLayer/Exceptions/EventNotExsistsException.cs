using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Exceptions
{
    public class EventNotExsistsException : Exception 
    {
        public EventNotExsistsException(string msg) : base(msg)
        {

        }
    }
}
