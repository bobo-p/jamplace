using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Interfaces.Services
{
    public interface IJamUserService : IService<IJamUser>
    {
        IJamUser GetByIdentityId(string Id);
    }
}
