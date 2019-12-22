using JamPlace.DataLayer.Entities;
using JamPlace.DomainLayer.Interfaces.Models;
using JamPlace.DomainLayer.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DataLayer.Repositories
{
    public class SongRepository: GenericRepository<ISong,SongDo>, ISongRepository
    {
        public SongRepository(ApplicationDbContext context): base(context)
        {

        }
    }
}
