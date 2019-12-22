using JamPlace.DataLayer.Entities;
using JamPlace.DomainLayer.Interfaces.Models;
using JamPlace.DomainLayer.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DataLayer.Repositories
{
    public class EquipmentRepository:GenericRepository<IEquipment,EquipmentDo>, IEquipmentRepository
    {
        public  EquipmentRepository(ApplicationDbContext context): base(context)
        {

        }
    }
}
