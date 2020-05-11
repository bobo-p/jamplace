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
    public class NeededEquipmentRepository :GenericRepository<IEquipment, NeededEquipmentDo>, INeededEquipmentRepository
    {
        private readonly IMapper _mapper;
        public NeededEquipmentRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public new IEquipment Add(IEquipment item)
        {
            var commentDo = new NeededEquipmentDo()
            {
                Name = item.Name,
                EventId = item.EventId,
                Date = item.Date
            };
            commentDo.UserId = item.JamUser.Id;
            var added = Context.Add(commentDo);
            Context.SaveChanges();
            return added.Entity;
        }
        public new void Update(IEquipment item)
        {
            var commentDo = Context.NeededEventEquipment.FirstOrDefault(p => p.Id == item.Id);
            commentDo.Name = item.Name;
            Context.Update(commentDo);
            Context.SaveChanges();
        }
        public new void Delete(IEquipment item)
        {
            var commentDo = Context.NeededEventEquipment.FirstOrDefault(p => p.Id == item.Id);
            Context.Remove(commentDo);
            Context.SaveChanges();
        }
        public IEnumerable<IEquipment> GetFilteredByEvent(int eventId)
        {
            var comments = Context.NeededEventEquipment.Where(comment => comment.EventId == eventId).Include(c => c.User).ToList();
            comments.ForEach(com => com.JamUser = com.User);
            return comments;
        }
    }
}
