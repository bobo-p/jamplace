using AutoMapper;
using JamPlace.DataLayer.Entities;
using JamPlace.DomainLayer.Interfaces.Models;
using JamPlace.DomainLayer.Interfaces.Repositories;
using JamPlace.DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamPlace.DataLayer.Repositories
{
    public class CommentRepository : GenericRepository<IComment, CommentDo>,ICommentRepository
    {
        private readonly IMapper _mapper;
        public CommentRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public new IComment Add(IComment item)
        {
            var commentDo = new CommentDo()
            {
                Content = item.Content,
                Date = item.Date,
                EventId = item.EventId,
            };
            commentDo.UserId = item.JamUser.Id;
            var added = Context.Add(commentDo);
            Context.SaveChanges();
            return added.Entity;
        }
        public new void Update(IComment item)
        {
            var commentDo = Context.Comments.FirstOrDefault(p => p.Id == item.Id);
            commentDo.Content = item.Content;         
            Context.Update(commentDo);
            Context.SaveChanges();
        }
        public new void Delete(IComment item)
        {
            var commentDo = Context.Comments.FirstOrDefault(p => p.Id == item.Id);
            Context.Remove(commentDo);
            Context.SaveChanges();
        }
        public IEnumerable<IComment> GetFilteredByEvent(int eventId)
        {
            var comments = Context.Comments.Where(comment => comment.EventId == eventId).Include(c => c.User).ToList();
            comments.ForEach(com => com.JamUser = com.User);
            return comments;
        }
    }
}
