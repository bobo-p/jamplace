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
                    .ThenInclude(x=>x.Equipment)
                 .FirstOrDefault(p=> p.Id==id);
            if (user == null) return null;
            user.PersonalEquipment = user.UserPersonalEquipment?.Select(p=> (IEquipment)p.Equipment).ToList();
            return user as IJamUser;
        }
        public new int Add(IJamUser item)
        {

            var doUser = _mapper.Map<JamUserDo>(item);
            doUser.UserPersonalEquipment = item.PersonalEquipment.Select(p => new PersonalEquipmentUserDo() { EquipmentDoId=p.Id,JamUser = doUser, JamUserDoId = item.Id, Equipment= _mapper.Map<EquipmentDo>(p) }).ToList();
            Context.Add(doUser);
            Context.SaveChanges();
            return doUser.Id;
        }
        public new void Update(IJamUser item)
        {
            var doUser = _mapper.Map<JamUserDo>(item);
            //mange PersonalEquipment relation
            var removedPersonalEquipmentRelations = new List<PersonalEquipmentUserDo>();
            var addedPersonalEquipmentRelations = new List<PersonalEquipmentUserDo>();
            var existingRelations = Context.Set<PersonalEquipmentUserDo>()?
                    .AsNoTracking()
                    .Where(p => p.JamUserDoId == doUser.Id).ToList();
            doUser.UserPersonalEquipment = item.PersonalEquipment?.Select(p => new PersonalEquipmentUserDo() { EquipmentDoId = p.Id, JamUser = doUser, JamUserDoId = item.Id, Equipment = _mapper.Map<EquipmentDo>(p) })?.ToList();
            removedPersonalEquipmentRelations = existingRelations.Except(doUser.UserPersonalEquipment).ToList();
            addedPersonalEquipmentRelations = doUser.UserPersonalEquipment.Except(existingRelations).ToList();
            //mange EventEquipment relation
            var removedEventEquipmentRelations = new List<JamUserEventEquipmentDo>();
            var addedEventEquipmentRelations = new List<JamUserEventEquipmentDo>();
            var existingEventEquipmentRelations = Context.Set<JamUserEventEquipmentDo>()?
                    .AsNoTracking()
                    .Where(p => p.JamUserDoId == doUser.Id).ToList();
            doUser.UserEventEquipment = item.EventEquipment?.Select(p => new JamUserEventEquipmentDo() { EventEquipmentDoId = p.Id, JamUser = doUser, JamUserDoId = item.Id, EventEquipment = _mapper.Map<UserEventEquipmentDo>(p) })?.ToList();
            removedEventEquipmentRelations = existingEventEquipmentRelations.Except(doUser.UserEventEquipment).ToList();
            addedEventEquipmentRelations = doUser.UserEventEquipment.Except(existingEventEquipmentRelations).ToList();


            Context.Update(doUser);
            Context.Set<PersonalEquipmentUserDo>().RemoveRange(removedPersonalEquipmentRelations);
            Context.Set<PersonalEquipmentUserDo>().AddRange(addedPersonalEquipmentRelations);
            Context.Set<JamUserEventEquipmentDo>().RemoveRange(removedEventEquipmentRelations);
            Context.Set<JamUserEventEquipmentDo>().AddRange(addedEventEquipmentRelations);
            
            Context.SaveChanges();
        }

        public IJamUser GetWithEventEq(int id, int eventId)
        {
            var user = Context.JamUsers.AsNoTracking()
               .Include(user => user.UserPersonalEquipment)
                   .ThenInclude(x => x.Equipment)
               .Include(user => user.UserEventEquipment)
                   .ThenInclude(x => x.EventEquipment)
                .FirstOrDefault(p => p.Id == id);
            if (user == null) return null;
            user.PersonalEquipment = user.UserPersonalEquipment?.Select(p => (IEquipment)p.Equipment).ToList();
            return user as IJamUser;
        }
    }
}
