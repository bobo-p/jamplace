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
    public class SongRepository: GenericRepository<ISong,SongDo>, ISongRepository
    {
        private readonly IMapper _mapper;
        public SongRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public new ISong Add(ISong item)
        {
            var songDo = _mapper.Map<SongDo>(item);
            songDo.EventId = _mapper.Map<JamEventDo>(item.JamEvent).Id;
            var data = Context.Add(songDo);
            Context.SaveChanges();
            Context.Entry(songDo).State = EntityState.Detached;
            return data.Entity;
        }
        public new void Delete(ISong item)
        {
            var toDelete = Context.Songs.FirstOrDefault(song => song.Id == item.Id);
            Context.Remove(toDelete);
            Context.SaveChanges();
        }
        public new void Update(ISong item)
        {
            var toUpdate = Context.Songs.FirstOrDefault(song => song.Id == item.Id);
            toUpdate.Artist = item.Artist;
            toUpdate.Title = item.Title;
            toUpdate.Link = item.Link;
            toUpdate.Description = item.Description;
            Context.Update(toUpdate);
            Context.SaveChanges();
        }
        public IEnumerable<ISong> GetFilteredByEvent(int eventId)
        {
            return Context.Songs.Where(song => song.EventId == eventId);
        }
    }
}
