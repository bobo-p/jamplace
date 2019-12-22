using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JamPlace.DataLayer.Entities
{
    public class AbstractParrentModelDo
    {
        [Key]
        public int Id { get; set; }
    }
}
