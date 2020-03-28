using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamPlace.DataLayer.Entities
{
    public  class CommentDo : AbstractParrentModelDo, IComment
    {
        public string Content { get; set; }
        public string UserId { get; set; }
    }
}
