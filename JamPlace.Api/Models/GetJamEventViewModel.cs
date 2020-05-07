using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JamPlace.Api.Models
{
    public class GetJamEventViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public AddressViewModel Address { get; set; }
        public ICollection<JamEventUserViewModel> Users { get; set; }
        public ICollection<SongViewModel> Songs { get; set; }
        public ICollection<EquipmentViewModel> NeededEquipment { get; set; }
        public ICollection<CommentViewModel> Comments { get; set; }
        public DateTime Date { get; set; }
    }
}
