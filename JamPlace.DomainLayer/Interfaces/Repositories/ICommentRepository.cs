﻿using JamPlace.DomainLayer.Interfaces.Models;
using JamPlace.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Interfaces.Repositories
{
    public interface ICommentRepository : IRepository<IComment>
    {
        IEnumerable<IComment> GetFilteredByEvent(int eventId);
    }
}
