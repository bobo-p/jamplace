using AutoMapper;
using JamPlace.DataLayer.Entities;
using JamPlace.DomainLayer.Interfaces.Models;
using JamPlace.DomainLayer.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamPlace.DataLayer.Repositories
{
    public class EventEquipmentRepository : GenericRepository<IUserEventEquipment, UserEventEquipmentDo>, IUserEventEquipmentRepository
    {
        private readonly IMapper _mapper;
        public EventEquipmentRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public new IUserEventEquipment Get(int id)
        {
            var eventEquipment = Context.EventEquipment.AsNoTracking()
                .Include(eventEq => eventEq.EventJamUsers)
                    .ThenInclude(x => x.JamUser)
                 .FirstOrDefault(p => p.Id == id);
            if (eventEquipment == null) return null;
            eventEquipment.JamUsers = eventEquipment.EventJamUsers?.Select(p => (IJamUser)p.JamUser).ToList();
            return eventEquipment as IUserEventEquipment;
        }
      
    }
}
