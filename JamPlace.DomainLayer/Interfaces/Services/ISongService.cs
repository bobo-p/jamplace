using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Interfaces.Services
{
    public interface ISongService : IService<ISong>
    {
        ISong Add(ISong song, int eventId);
        void Edit(ISong song, int eventId);
        IEnumerable<ISong> GetFilteredByTitle(string seacrhText, int eventId);
    }
}
