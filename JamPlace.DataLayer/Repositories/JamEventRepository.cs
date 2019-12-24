using AutoMapper;
using JamPlace.DataLayer.Entities;
using JamPlace.DomainLayer.Interfaces.Models;
using JamPlace.DomainLayer.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace JamPlace.DataLayer.Repositories
{
    public class JamEventRepository : GenericRepository<IJamEvent,JamEventDo>, IJamEventRepository
    {
        private readonly IMapper _mapper;
        public JamEventRepository(ApplicationDbContext context): base(context)
        {
            
        }
        public new IJamEvent Get(int id)
        {
            var jamEvent = Context.JamEvents.AsNoTracking().Where(p=>p.Id==id)
                .Include(ev => ev.JamEventJamUser)
                    .ThenInclude(x => x.JamUser)
                .Include(ev => ev.NeededEventEquipment)
                    .ThenInclude(x => x.Equipment)
                 .FirstOrDefault();
            if (jamEvent == null) return null;
            jamEvent.Users = jamEvent.JamEventJamUser?.Select(p => (IJamUser)p.JamUser).ToList();
            return jamEvent as IJamEvent;
        }
        public new void Update(IJamEvent item)
        {
            var doEvent = _mapper.Map<JamEventDo>(item);

            doEvent.JamEventJamUser = doEvent.Users?.Select(p => new JamEventJamUserDo() { JamUserDoId = p.Id, JamUser = _mapper.Map<JamUserDo>(p), JamEventDoId = item.Id, JamEvent = doEvent })?.ToList();
            doEvent.NeededEventEquipment = doEvent.NeededEquipment?.Select(p => new NeededEquipmentEventDo() { Equipment= _mapper.Map<EquipmentDo>(p),EquipmentDoId=p.Id, JamEventDoId = item.Id, JamEvent = doEvent })?.ToList();


            //mange JamEventJamUser relation
            var removedJamEventtRelations = new List<JamEventJamUserDo>();
            var addedJamEventRelations = new List<JamEventJamUserDo>();
            var existintJamEventRelations = Context.Set<JamEventJamUserDo>()?
                    .AsNoTracking()
                    .Where(p => p.JamEventDoId == doEvent.Id).ToList();
            removedJamEventtRelations = existintJamEventRelations.Except(doEvent.JamEventJamUser).ToList();
            addedJamEventRelations = doEvent.JamEventJamUser.Except(existintJamEventRelations).ToList();

            //mange NeedeEventEquipment relation
            var removeNeedeEventEquipmentRelations = new List<NeededEquipmentEventDo>();
            var addedNeedeEventEquipmentRelations = new List<NeededEquipmentEventDo>();
            var existintNeedeEventEquipmentRelations = Context.Set<NeededEquipmentEventDo>()?
                    .AsNoTracking()
                    .Where(p => p.JamEventDoId == doEvent.Id).ToList();
            removeNeedeEventEquipmentRelations = existintNeedeEventEquipmentRelations.Except(doEvent.NeededEventEquipment).ToList();
            addedNeedeEventEquipmentRelations = doEvent.NeededEventEquipment.Except(existintNeedeEventEquipmentRelations).ToList();

            Context.Update(doEvent);
            Context.Set<JamEventJamUserDo>().RemoveRange(removedJamEventtRelations);
            Context.Set<JamEventJamUserDo>().AddRange(addedJamEventRelations);
            Context.Set<NeededEquipmentEventDo>().RemoveRange(removeNeedeEventEquipmentRelations);
            Context.Set<NeededEquipmentEventDo>().AddRange(addedNeedeEventEquipmentRelations);


            Context.SaveChanges();
        }


    }
}
