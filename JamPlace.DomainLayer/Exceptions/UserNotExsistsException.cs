using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Exceptions
{
    public class UserNotExsistsException : Exception
    {
        public UserNotExsistsException(string msg) : base(msg)
        {

        }
    }
}
