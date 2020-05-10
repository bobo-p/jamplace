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
    public class JamUserRepository : GenericRepository<IJamUser, JamUserDo>, IJamUserRepository
    {
        private readonly IMapper _mapper;
        public JamUserRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public new IJamUser Get(int id)
        {
            var user = Context.JamUsers.AsNoTracking()
                .Include(user => user.UserPersonalEquipment)
                    .ThenInclude(x => x.Equipment)
                 .FirstOrDefault(p => p.Id == id);
            if (user == null) return null;
            user.PersonalEquipment = user.UserPersonalEquipment?.Select(p => (IEquipment)p.Equipment).ToList();
            return user as IJamUser;
        }
        public new IJamUser Add(IJamUser item)
        {

            var doUser = _mapper.Map<JamUserDo>(item);
            doUser.UserPersonalEquipment = item?.PersonalEquipment?.Select(p => new PersonalEquipmentUserDo() { EquipmentDoId = p.Id, JamUser = doUser, JamUserDoId = item.Id, Equipment = _mapper.Map<EquipmentDo>(p) }).ToList();
            Context.Add(doUser);
            Context.SaveChanges();
            return doUser;
        }
        public new void Update(IJamUser item)
        {
            var doUser = _mapper.Map<JamUserDo>(item);
            Context.Update(doUser);
            Context.SaveChanges();
        }
        public void ComplexUpdate(IJamUser item)
        {
            var doUser = _mapper.Map<JamUserDo>(item);
            //mange PersonalEquipment relation
            var removedPersonalEquipmentRelations = new List<PersonalEquipmentUserDo>();
            var addedPersonalEquipmentRelations = new List<PersonalEquipmentUserDo>();
            var existingRelations = Context.Set<PersonalEquipmentUserDo>()?
                    .AsNoTracking()
                    .Where(p => p.JamUserDoId == doUser.Id).ToList();
            doUser.UserPersonalEquipment = item.PersonalEquipment?.Select(p => new PersonalEquipmentUserDo() { EquipmentDoId = p.Id, JamUser = doUser, JamUserDoId = item.Id, Equipment = _mapper.Map<EquipmentDo>(p) })?.ToList();
            doUser.JamEventJamUser = item.JamEvents?.Select(p => new JamEventJamUserDo() { JamEventDoId = p.Id, JamUser = doUser, JamUserDoId = item.Id, JamEvent = _mapper.Map<JamEventDo>(p) })?.ToList();
            doUser.EventEquipmentDos = doUser.EventEquipment?.Select(p=>new EventEquipmentDo() {Id=p.Id,JamEventId=p.JamEventId,JamUserId=p.JamUserId } )?.ToList();
            foreach (var eventEq in doUser.EventEquipmentDos)

                eventEq.Equpiment = doUser.EventEquipment.FirstOrDefault(p => p.Id == eventEq.Id)?.Equpiment;
          
            removedPersonalEquipmentRelations = existingRelations.Except(doUser.UserPersonalEquipment).ToList();
            addedPersonalEquipmentRelations = doUser.UserPersonalEquipment.Except(existingRelations).ToList();
            //mange list of EventEquipment relation
            var removedEventEquipmentRelations = new List<EventEquipmentDo>();
            var addedEventEquipmentRelations = new List<EventEquipmentDo>();
            var existingEventEquipmentRelations = Context.EventEquipment?
                    .AsNoTracking()
                    .Where(p => p.JamUserId == doUser.Id).ToList();
            //doUser.UserEventEquipment = item.EventEquipment?.Select(p => new JamUserEventEquipmentDo() { EventEquipmentDoId = p.Id, JamUser = doUser, JamUserDoId = item.Id, EventEquipment = _mapper.Map<EventEquipmentDo>(p) })?.ToList();
            removedEventEquipmentRelations = existingEventEquipmentRelations.Except(doUser.EventEquipmentDos).ToList();
            addedEventEquipmentRelations = doUser.EventEquipmentDos.Except(existingEventEquipmentRelations).ToList();

            //mange JamEventJamUser relation
            var removedJamEventtRelations = new List<JamEventJamUserDo>();
            var addedJamEventRelations = new List<JamEventJamUserDo>();
            var existintJamEventRelations = Context.Set<JamEventJamUserDo>()?
                    .AsNoTracking()
                    .Where(p => p.JamUserDoId == doUser.Id).ToList();
            if(doUser?.JamEventJamUser!=null)
                removedJamEventtRelations = existintJamEventRelations.Except(doUser?.JamEventJamUser).ToList();
            if (doUser?.JamEventJamUser != null)
                addedJamEventRelations = doUser.JamEventJamUser.Except(existintJamEventRelations).ToList();

            Context.Update(doUser);
            Context.Set<JamEventJamUserDo>().RemoveRange(removedJamEventtRelations);
            Context.Set<JamEventJamUserDo>().AddRange(addedJamEventRelations);
            Context.Set<PersonalEquipmentUserDo>().RemoveRange(removedPersonalEquipmentRelations);
            Context.Set<PersonalEquipmentUserDo>().AddRange(addedPersonalEquipmentRelations);
            Context.EventEquipment.RemoveRange(removedEventEquipmentRelations);
            Context.EventEquipment.AddRange(addedEventEquipmentRelations);

            Context.SaveChanges();
        }

        public IJamUser GetWithEventEq(int id, int eventId)
        {

            var user = Context.JamUsers?.AsNoTracking().Where(u =>u.Id==id)
                .Select(e => new {
                    e,//for later projection
                    e.PersonalEquipment,//cache PersonalEquipment
                    EventEquipmentDos = e.EventEquipmentDos.Where(equip=>equip.JamEventId==eventId)

                }).Select(e=>e.e)
               ?.Include(user => user.UserPersonalEquipment)
                   .ThenInclude(x => x.Equipment)
               .Include(user => user.EventEquipmentDos)
                   .ThenInclude(x => x.UserEventEventEqupiment)
                    .ThenInclude(x => x.Equipment)
                .FirstOrDefault();
            if (user == null) return null;
            user.PersonalEquipment = user.UserPersonalEquipment?.Select(p => (IEquipment)p.Equipment)?.ToList();
            if (user.EventEquipmentDos != null)
            {
                user.EventEquipment = user.EventEquipmentDos.Select(p => (IEventEquipment)p)?.ToList();
                foreach (var el in user.EventEquipment)
                    el.Equpiment = user.EventEquipmentDos.FirstOrDefault()?.UserEventEventEqupiment?.Select(p => (IEquipment)p.Equipment)?.ToList();
            }
            
            return user as IJamUser;
        }

        public IEnumerable<IJamEvent> GetEventsFilteredPageByUserId(int pageIndex, int pageSize, bool orderByDate, string city, int userId)
        {
            var user = Context.JamUsers?.AsNoTracking().Where(u => u.Id == userId)
                .Include(x => x.JamEventJamUser)
                    .ThenInclude(x => x.JamEvent).FirstOrDefault();
            user.JamEvents = user.JamEventJamUser?.Select(p=>p.JamEvent).ToList();

            return user.JamEvents;
        }

        public IJamUser GetByIdentityId(string Id)
        {
            return Context.JamUsers?.AsNoTracking().FirstOrDefault(u => u.UserIdentityId == Id);
        }
    }
}
