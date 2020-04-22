using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JamPlace.Api.Models;
using JamPlace.DomainLayer.Interfaces.Services;
using JamPlace.DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JamPlace.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JamEventController : ControllerBase
    {
        private readonly IJamEventService _jamEventService;
        private readonly IMapper _mapper;

        public JamEventController(IJamEventService jamEventService, IMapper mapper)
        {
            _jamEventService = jamEventService;
            _mapper = mapper;
        }
        [HttpPost("AddJamEvent")]
        public IActionResult AddJamEvent(AddJamEventViewModel jamEventInfo)
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
        [HttpGet("GetEvent/{id}")]
        public GetJamEventViewModel GetEvent(int id)
        {
            var getJamEvent = _jamEventService.Get(id);
            return _mapper.Map<GetJamEventViewModel>(getJamEvent);
        }
    }
}