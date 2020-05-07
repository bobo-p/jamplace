using JamPlace.DomainLayer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JamPlace.DataLayer.Entities
{
    public  class CommentDo : AbstractParrentModelDo, IComment
    {
        public string Content { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("Id")]
        public int EventId { get; set; }
        public JamEventDo Event { get; set; }
        [ForeignKey("Id")]
        public int UserId { get; set; }
        public JamUserDo User { get; set; }
        [NotMapped]
        public IJamUser JamUser { get; set; }
    }
}
