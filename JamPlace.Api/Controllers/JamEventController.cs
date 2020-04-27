using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using JamPlace.Api.Helpers;
using JamPlace.Api.Models;
using JamPlace.DomainLayer.Interfaces.Models;
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
        private readonly IJamUserService _jamUserService;
        private readonly IMapper _mapper;

        public JamEventController(IJamEventService jamEventService, IMapper mapper, IJamUserService jamUserService)
        {
            _jamEventService = jamEventService;
            _mapper = mapper;
            _jamUserService = jamUserService;
        }
        [HttpPost("AddJamEvent")]
        public IActionResult AddJamEvent(AddJamEventViewModel jamEventInfo)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _jamUserService.GetByIdentityId(userId);

            var jamEvent = JamEventBuilder.BuildJamEventFromViewModels(jamEventInfo);
            jamEvent.Users = new List<IJamUser>() { user };

            var addedEvent = _jamEventService.Add(jamEvent);

            return Ok(addedEvent.Id);
        }
        [HttpGet("GetEvent/{id}")]
        public GetJamEventViewModel GetEvent(int id)
        {
            var getJamEvent = _jamEventService.Get(id);
            return _mapper.Map<GetJamEventViewModel>(getJamEvent);
        }
    }
}