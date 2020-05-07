using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JamPlace.Api.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserName{ get; set; }
        public DateTime Date { get; set; }
        public int EventId { get; set; }
    }
}
