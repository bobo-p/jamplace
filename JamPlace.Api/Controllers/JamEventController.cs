using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JamPlace.Api.Models;
using JamPlace.DomainLayer.Interfaces.Services;
using JamPlace.DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JamPlace.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JamEventController : ControllerBase
    {
        private readonly IJamEventService _jamEventService;

        public JamEventController(IJamEventService jamEventService)
        {
            _jamEventService = jamEventService;
        }
        [HttpPost("AddJamEvent")]
        public IActionResult AddJamEvent(JamEventViewModel jamEventInfo)
        {
            _jamEventService.Add(new JamEvent()
            {
                Name=jamEventInfo.Name,
                Size=jamEventInfo.Size,
                Description=jamEventInfo.Description,
                Adress = new Adress()
                {
                    City = jamEventInfo.Address.City,
                    LocalNumber = jamEventInfo.Address.LocalNumber,
                    Street = jamEventInfo.Address.Street,
                    Country = jamEventInfo.Address.Country,
                                        
                    
                }
            });
            return Ok("{\"data\" :\"String ok\"}");
        }
    }
}