using JamPlace.DataLayer.Entities;
using JamPlace.DomainLayer.Interfaces.Models;
using JamPlace.DomainLayer.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DataLayer.Repositories
{
    public class NeededEquipmentEventRepository : GenericRepository<INeededEquipmentEvent,NeededEquipmentEventDo>, INeededEquipmentEventRepository
    {
        public NeededEquipmentEventRepository(ApplicationDbContext context): base(context)
        {

        }
    }
}
