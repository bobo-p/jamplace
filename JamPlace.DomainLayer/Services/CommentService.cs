using JamPlace.DomainLayer.Interfaces.Models;
using JamPlace.DomainLayer.Interfaces.Repositories;
using JamPlace.DomainLayer.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamPlace.DomainLayer.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IJamEventRepository _jamEventRepository;
        private readonly IJamUserRepository _jamUserRepository;

        public CommentService(ICommentRepository commentRepository, IJamEventRepository jamEventRepository, IJamUserRepository jamUserRepository)
        {
            _commentRepository = commentRepository;
            _jamEventRepository = jamEventRepository;
            _jamUserRepository = jamUserRepository;
        }

        public IComment Add(IComment comment, string userIdentityId)
        {
            var user = _jamUserRepository.GetByIdentityId(userIdentityId);
            comment.Date = DateTime.Now;
            comment.JamUser = user;
            var addedUser = _commentRepository.Add(comment);
            addedUser.JamUser = user;
            return addedUser;
        }

        public IComment Add(IComment item)
        {
            throw new NotImplementedException();
        }

        public void Delete(IComment item)
        {
            _commentRepository.Delete(item);
        }

        public void Edit(IComment comment, int eventId)
        {
            var jamEvent = _jamEventRepository.Get(eventId);
            //TODO check if user has acces
            _commentRepository.Update(comment);
        }

        public void Edit(IComment item)
        {
            throw new NotImplementedException();
        }

        public IComment Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IComment> GetFilteredByUserName(string seacrhText, int eventId)
        {
            var comments = _commentRepository.GetFilteredByEvent(eventId).ToList();
            if (string.IsNullOrEmpty(seacrhText)) return comments.OrderByDescending(p => p.Date);
            return comments.Where(ev => !string.IsNullOrEmpty(ev.JamUser.UserName) && ev.JamUser.UserName.ToLower().Contains(seacrhText.ToLower())).OrderByDescending(p => p.Date);
        }
    }
}
