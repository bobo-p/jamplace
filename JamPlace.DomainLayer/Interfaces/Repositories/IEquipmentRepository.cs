﻿using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Interfaces.Repositories
{
    public interface IEquipmentRepository : IRepository<IEquipment>
    {
        IEnumerable<IEquipment> GetFilteredByEvent(int eventId);
    }
}
