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

            var data = Context.Add(jamEventDo);
            Context.SaveChanges();
            Context.Entry(jamEventDo).State = EntityState.Detached;
            return data.Entity;
        }
        public new IJamEvent Get(int id)
        {
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var jamEvent = Context.JamEvents.AsNoTracking().Where(p=>p.Id==id)
                .Include(ev => ev.JamEventJamUser)
                    .ThenInclude(x => x.JamUser)
                .Include(ev => ev.NeededEventEquipment)
                    .ThenInclude(x => x.Equipment)                
                .Include(ev => ev.EventAdress)
                .Include(ev => ev.SongsDo)
                .Include(ev => ev.CommentsDo)
                    .ThenInclude(x => x.User)
                 .FirstOrDefault();

            if (jamEvent == null) return null;
            jamEvent.Users = jamEvent.JamEventJamUser?.Select(p => (IJamUser)p.JamUser).ToList();
            jamEvent.Songs = jamEvent.SongsDo?.OrderByDescending(item => item.AddDate).Select(CastToIsong).ToList();
            jamEvent.Address = (IAdress)jamEvent.EventAdress;
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
                .Include(ev => ev.NeededEventEquipment)
                    .ThenInclude(x => x.Equipment)
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
                outputList.Add(jamEvent);
            }
            return outputList;
        }

        public UserAccessModeEnum GetAccesTypeForUser(int eventId, string userId)
        {
            var jamEvent = Context.JamEvents.AsNoTracking().Where(p => p.Id == eventId)
                .Include(ev => ev.JamEventJamUser)
                .ThenInclude(p => p.JamUser)
                .FirstOrDefault();
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
            if(item.Users != null)
            {
                
            }
            Context.Update(doEvent);
            Context.SaveChanges();
            Context.Entry(doEvent).State = EntityState.Detached;
        }
        public new void Update(IJamEvent item)
        {
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
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
            addedJamEventRelations.ForEach(p => p.AccessMode = Common.UserAccessModeEnum.Guest);

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
