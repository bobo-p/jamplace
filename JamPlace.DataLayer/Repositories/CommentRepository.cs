using JamPlace.DataLayer.Entities;
using JamPlace.DomainLayer.Interfaces.Models;
using JamPlace.DomainLayer.Interfaces.Repositories;
using JamPlace.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DataLayer.Repositories
{
    public class CommentRepository : GenericRepository<IComment, CommentDo>,ICommentRepository
    {
        public CommentRepository(ApplicationDbContext context):base(context)
        {

        }
    }
}
