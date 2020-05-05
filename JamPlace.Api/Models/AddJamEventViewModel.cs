using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JamPlace.Api.Models
{
    public class AddJamEventViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public AddressViewModel Address { get; set; }
        public DateTime Date { get; set; }
    }
}
