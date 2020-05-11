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

                 .FirstOrDefault(p => p.Id == id);
            if (user == null) return null;
            return user as IJamUser;
        }
        public new IJamUser Add(IJamUser item)
        {

            var doUser = _mapper.Map<JamUserDo>(item);
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
            Context.SaveChanges();
        }

        public IJamUser GetWithEventEq(int id, int eventId)
        {

            var user = Context.JamUsers?.AsNoTracking().Where(u =>u.Id==id)
                .Include(x => x.ProvidedEquipmentDos)
                .Include(x => x.NeededEquipmentDos)

                .FirstOrDefault();
            if (user == null) return null;       
            
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
