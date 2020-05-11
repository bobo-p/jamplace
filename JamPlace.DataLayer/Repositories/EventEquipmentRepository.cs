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
    public class EventEquipmentRepository : GenericRepository<IEventEquipment, EventEquipmentDo>, IUserEventEquipmentRepository
    {
        private readonly IMapper _mapper;
        public EventEquipmentRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public new IEventEquipment Get(int id)
        {
            var eventEquipment = Context.EventEquipment.AsNoTracking()
                .Include(eventEq => eventEq.Event)
                 .FirstOrDefault(p => p.Id == id);
            if (eventEquipment == null) return null;
           
            return eventEquipment as IEventEquipment;
        }
      
    }
}
