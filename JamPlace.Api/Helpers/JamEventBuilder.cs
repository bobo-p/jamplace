using JamPlace.Api.Models;
using JamPlace.DomainLayer.Interfaces.Models;
using JamPlace.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JamPlace.Api.Helpers
{
    public class JamEventBuilder
    {
        public static IJamEvent BuildJamEventFromViewModels(AddJamEventViewModel addJamEvent)
        {
            return new JamEvent()
            {
                Name = addJamEvent.Name,
                Size = addJamEvent.Size,
                Description = addJamEvent.Description,
                Adress = new Adress()
                {
                    City = addJamEvent.Address.City,
                    LocalNumber = addJamEvent.Address.LocalNumber,
                    Street = addJamEvent.Address.Street,
                    Country = addJamEvent.Address.Country,
                }
            };
        }
    }
}
