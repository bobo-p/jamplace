using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DomainLayer.Interfaces.Services
{
    public interface ICommentService : IService<IComment>
    {
        IComment Add(IComment comment, string userIdentityId);
        void Edit(IComment comment, int eventId);
        IEnumerable<IComment> GetFilteredByUserName(string seacrhText, int eventId);
    }
}
