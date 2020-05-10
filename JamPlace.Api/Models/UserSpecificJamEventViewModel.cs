using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JamPlace.Api.Models
{
    public class UserSpecificJamEventViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public AddressViewModel Adress { get; set; }
        public DateTime Date { get; set; }
        public JamUserViewModel Creator { get; set; }
    }
}
