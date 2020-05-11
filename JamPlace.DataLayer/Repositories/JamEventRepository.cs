using AutoMapper;
using JamPlace.DataLayer.Entities;
using JamPlace.DomainLayer.Common;
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
        public JamEventRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public new IJamEvent Add(IJamEvent item)
        {
            var jamEventDo = new JamEventDo()
            {
                Date = item.Date,
                Size = item.Size,
                Name = item.Name,
                Description = item.Description,
            };

            jamEventDo.EventAdress = new AdressDo()
            {
                City = item.Address?.City,
                Country = item.Address?.Country,
                LocalNumber = item.Address?.LocalNumber,
                Street = item.Address?.Street
            };
            jamEventDo = Context.Add(jamEventDo).Entity;

            jamEventDo.JamEventJamUser = new List<JamEventJamUserDo>();
            if (item.Users != null)
            {
                foreach (var user in item.Users)
                {
                    var jamEventJamUser = new JamEventJamUserDo()
                    {
                        JamEvent = jamEventDo,
                        JamUser = Context.JamUsers.FirstOrDefault(usr => usr.Id == user.Id),
                        AccessMode = Common.UserAccessModeEnum.Creator
                    };
                    jamEventDo.JamEventJamUser.Add(jamEventJamUser);
                }
            }

            
            Context.SaveChanges();
            Context.Entry(jamEventDo).State = EntityState.Detached;
            return jamEventDo;
        }
        public new IJamEvent Get(int id)
        {
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var jamEvent = GetDoById(id);
            if (jamEvent == null) return null;
            jamEvent.Users = jamEvent.JamEventJamUser?.Select(p => (IJamUser)p.JamUser).ToList();
            jamEvent.Creator = jamEvent.JamEventJamUser?.Where(p => p.AccessMode == Common.UserAccessModeEnum.Creator)?.Select(p => (IJamUser)p.JamUser).FirstOrDefault();
            jamEvent.Songs = jamEvent.SongsDo?.OrderByDescending(item => item.AddDate).Select(CastToIsong).ToList();
            jamEvent.Address = (IAdress)jamEvent.EventAdress;
            jamEvent?.CommentsDo?.ToList()?.ForEach(comment => comment.JamUser = comment?.User);
            jamEvent.Comments = jamEvent.CommentsDo?.Select(p => (IComment)p).ToList();
            jamEvent?.NeededEquipmentDos?.ToList()?.ForEach(p => p.JamUser = p?.User);
            jamEvent.NeededEquipment = jamEvent.NeededEquipmentDos?.Select(p => (IEquipment)p).ToList();

            jamEvent?.ProvidedEquipmentDos?.ToList()?.ForEach(p => p.JamUser = p?.User);
            jamEvent.ProvidedEquipment = jamEvent.ProvidedEquipmentDos?.Select(p => (IEquipment)p).ToList();

            jamEvent?.CommentsDo?.ToList()?.ForEach(comment => comment.JamUser = comment?.User);
            jamEvent.Comments = jamEvent.CommentsDo?.Select(p => (IComment)p).ToList();
            return jamEvent as IJamEvent;
        }
        public new IEnumerable<IJamEvent> GetAll()
        {
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var jamEvents = Context.JamEvents.AsNoTracking()
                .Include(ev => ev.JamEventJamUser)
                    .ThenInclude(x => x.JamUser)
                .Include(ev => ev.EventAdress)
                .Include(ev => ev.SongsDo)
                .Include(ev => ev.CommentsDo)
                    .ThenInclude(x => x.User);

            if (jamEvents == null) return null;
            var outputList = new List<IJamEvent>();
            foreach (var jamEvent in jamEvents)
            {
                jamEvent.Users = jamEvent.JamEventJamUser?.Select(p => (IJamUser)p.JamUser).ToList();
                jamEvent.Songs = jamEvent.SongsDo?.OrderByDescending(item => item.AddDate).Select(CastToIsong).ToList();
                jamEvent.Address = (IAdress)jamEvent.EventAdress;
                jamEvent?.CommentsDo?.ToList()?.ForEach(comment => comment.JamUser = comment?.User);
                jamEvent.Comments = jamEvent.CommentsDo?.Select(p => (IComment)p).ToList();
                jamEvent.Creator = jamEvent?.JamEventJamUser.Where(p => p.AccessMode == Common.UserAccessModeEnum.Creator)?.Select(p => (IJamUser)p.JamUser).FirstOrDefault();
                outputList.Add(jamEvent);
            }
            return outputList;
        }

        public UserAccessModeEnum GetAccesTypeForUser(int eventId, string userId)
        {
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var jamEvent = Context.JamEvents.AsNoTracking().Where(p => p.Id == eventId)
                .Include(ev => ev.JamEventJamUser)
                .ThenInclude(p => p.JamUser)
                .FirstOrDefault();
            Context.Entry(jamEvent).State = EntityState.Detached;
            var jamEventJamUser = jamEvent?.JamEventJamUser?.FirstOrDefault(usr => usr?.JamUser.UserIdentityId == userId);
            return GetDomainAccesModeFromJamEventJamUser(jamEventJamUser);
        }
        public void GrantGuestAccessUser(int eventId, int userId)
        {
            var jamEventJamUser = new JamEventJamUserDo()
            {
                AccessMode = Common.UserAccessModeEnum.Guest,
                JamEventDoId = eventId,
                JamUserDoId = userId
            };
            Context.Add(jamEventJamUser);
            Context.SaveChanges();
            Context.Entry(jamEventJamUser).State = EntityState.Detached;
        }
        public void RemoveAccessForUser(int eventId, int userId)
        {          
            var entity = GetDoById(eventId);
            var jamUserjamEvents = entity.JamEventJamUser?.Where(p => p.JamEventDoId == eventId && p.JamUserDoId == userId && p.AccessMode != Common.UserAccessModeEnum.Creator).ToList();
            jamUserjamEvents.ForEach(prop => prop.JamEvent = null);
            jamUserjamEvents.ForEach(prop => prop.JamUser = null);
            Context.RemoveRange(jamUserjamEvents);
            Context.SaveChanges();
        }
        public IEnumerable<IJamEvent> GetFiltereByUser(string userId)
        {
            var userDo = Context.JamUsers.AsNoTracking().Where(user => user.UserIdentityId == userId)
                .Include(ev => ev.JamEventJamUser)
                    .ThenInclude(ev => ev.JamEvent)                  
                    .ThenInclude(x => x.EventAdress)
                 .FirstOrDefault();
            if (userDo == null) return null;
            userDo.JamEvents = userDo.JamEventJamUser?.Select(p => p.JamEvent).ToList();
            userDo.JamEvents?.ToList().ForEach(jamEvent => jamEvent.Address = ((JamEventDo)jamEvent).EventAdress);
            foreach(var ev in userDo.JamEvents)
            {
                var jamEvent = Context.JamEvents.AsNoTracking().Where(p => p.Id == ev.Id)
                    .Include(x => x.JamEventJamUser)
                    .ThenInclude(x => x.JamUser)
                    .FirstOrDefault() ;
                ev.Creator = jamEvent?.JamEventJamUser.Where(p => p.AccessMode == Common.UserAccessModeEnum.Creator)?.Select(p => (IJamUser)p.JamUser).FirstOrDefault();
            }
            return userDo?.JamEvents;
        }

        public IEnumerable<IJamEvent> GetFilteredPage(int pageIndex, int pageSize, bool orderByDate, string city)
        {
            var jamEvents = Context.JamEvents?.AsNoTracking()
                .Include(ev => ev.Address)
                .Where(p => (p.Address==null || string.IsNullOrEmpty(city)) ? true : p.Address.City.ToLower().Contains(city.ToLower())).Skip(pageIndex*pageSize).Take(pageSize);
            if (jamEvents == null)
                return null;
            if (orderByDate)
                jamEvents = jamEvents.OrderBy(p=>p.Date);
            return jamEvents?.ToList();
        }
        public void SimpleUpdate(IJamEvent item)
        {
            var doEvent = _mapper.Map<JamEventDo>(item);
            doEvent.EventAdress = _mapper.Map<AdressDo>(item.Address);
            Context.Update(doEvent);
            Context.SaveChanges();
            Context.Entry(doEvent).State = EntityState.Detached;
        }
        public void Delete(int id)
        {
            var entity = GetDoById(id);
            Context.Entry(entity).State = EntityState.Deleted;
            Context.JamEvents.Remove(entity);
            var jamUserjamEvents = entity.JamEventJamUser?.Where(p => p.JamEventDoId == id);
            Context.RemoveRange(jamUserjamEvents);
            Context.SaveChanges();
        }
        public new void Update(IJamEvent item)
        {
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var doEvent = _mapper.Map<JamEventDo>(item);

            doEvent.JamEventJamUser = doEvent.Users?.Select(p => new JamEventJamUserDo() { JamUserDoId = p.Id, JamUser = _mapper.Map<JamUserDo>(p), JamEventDoId = item.Id, JamEvent = doEvent })?.ToList();

            //mange JamEventJamUser relation
            var removedJamEventtRelations = new List<JamEventJamUserDo>();
            var addedJamEventRelations = new List<JamEventJamUserDo>();
            var existintJamEventRelations = Context.Set<JamEventJamUserDo>()?
                    .AsNoTracking()
                    .Where(p => p.JamEventDoId == doEvent.Id).ToList();
            removedJamEventtRelations = existintJamEventRelations.Except(doEvent.JamEventJamUser).ToList();
            addedJamEventRelations = doEvent.JamEventJamUser.Except(existintJamEventRelations).ToList();
            addedJamEventRelations.ForEach(p => p.AccessMode = Common.UserAccessModeEnum.Guest);           

            Context.Update(doEvent);
            Context.Set<JamEventJamUserDo>().RemoveRange(removedJamEventtRelations);
            Context.Set<JamEventJamUserDo>().AddRange(addedJamEventRelations);


            Context.SaveChanges();
        }
        private JamEventDo GetDoById(int id)
        {
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return Context.JamEvents.AsNoTracking().Where(p => p.Id == id)
                .Include(ev => ev.JamEventJamUser)
                    .ThenInclude(x => x.JamUser)
                .Include(ev => ev.NeededEquipmentDos)
                    .ThenInclude(x => x.User)
                .Include(ev => ev.ProvidedEquipmentDos)
                    .ThenInclude(x => x.User)
                .Include(ev => ev.EventAdress)
                .Include(ev => ev.SongsDo)
                .Include(ev => ev.CommentsDo)
                    .ThenInclude(x => x.User).AsNoTracking()
                 .FirstOrDefault();
        }
        private UserAccessModeEnum GetDomainAccesModeFromJamEventJamUser(JamEventJamUserDo jamEventJamUser)
        {
            if (jamEventJamUser == null)
                return UserAccessModeEnum.None;
            return (UserAccessModeEnum)jamEventJamUser.AccessMode;
        }
        private ISong CastToIsong(SongDo song)
        {
            song.JamEvent = song.Event;
            return (ISong)song;
        }    
    }
}
