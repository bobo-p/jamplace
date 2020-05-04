using JamPlace.DomainLayer.Interfaces.Models;
using JamPlace.DomainLayer.Interfaces.Repositories;
using JamPlace.DomainLayer.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamPlace.DomainLayer.Services
{
    public class SongService : ISongService
    {
        private readonly ISongRepository _songRepository;
        private readonly IJamEventRepository _jamEventRepository;
        public SongService(ISongRepository songRepository, IJamEventRepository jamEventRepository)
        {
            _songRepository = songRepository;
            _jamEventRepository = jamEventRepository;
        }
        public ISong Add(ISong item)
        {
            throw new NotImplementedException();
        }

        public ISong Add(ISong song, int eventId)
        {
            var jamEvent = _jamEventRepository.Get(eventId);
            song.JamEvent = jamEvent;
            song.AddDate = DateTime.Now;
            return _songRepository.Add(song);
        }

        public void Delete(ISong item)
        {
            _songRepository.Delete(item);
        }

        public void Edit(ISong item)
        {
            throw new NotImplementedException();
        }

        public void Edit(ISong song, int eventId)
        {
            var jamEvent = _jamEventRepository.Get(eventId);
            //TODO check if user has acces
             _songRepository.Update(song);
        }

        public ISong Get(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<ISong> GetFilteredByTitle(string seacrhText, int eventId)
        {
            var songs = _songRepository.GetFilteredByEvent(eventId).ToList();
            if (string.IsNullOrEmpty(seacrhText)) return songs.OrderByDescending(p => p.AddDate);
            return songs.Where(ev => ev.Title.ToLower().Contains(seacrhText.ToLower())).OrderByDescending(p => p.AddDate);
        }
    }
}
