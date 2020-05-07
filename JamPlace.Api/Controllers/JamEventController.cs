using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using JamPlace.Api.Filters;
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
    [Authorize]
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
        [HttpGet("JoinJamEvent/{eventId}")]
        public IActionResult JoinJamEvent(int eventId)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            _jamEventService.Join(eventId, userId);

            return Ok();
        }
        [HttpPost("UpdateJamEvent")]
        public IActionResult UpdateJamEvent(AddJamEventViewModel jamEventInfo)
        {

            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _jamUserService.GetByIdentityId(userId);

            var jamEvent = JamEventBuilder.BuildJamEventFromViewModels(jamEventInfo);
            jamEvent.Users = new List<IJamUser>() { user };

            _jamEventService.Edit(jamEvent);

            return Ok();
        }
        [HttpGet("GetEvent/{id}")]
        [ServiceFilter(typeof(UserAccessFilter))]
        public GetJamEventViewModel GetEvent(int id)
        {
            var getJamEvent = _jamEventService.Get(id);
            getJamEvent.Date = getJamEvent.Date.ToLocalTime();
            var model = _mapper.Map<GetJamEventViewModel>(getJamEvent);
            return model;
        }
        [HttpGet("GetEvents")]
        public IEnumerable<GetJamEventViewModel> GetEvents()
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var getJamEvents = _jamEventService.GetAllNotJoined(userId).ToList();
            getJamEvents.ForEach(prop => prop.Date = prop.Date.ToLocalTime());
            var models = getJamEvents.Select(_mapper.Map<GetJamEventViewModel>);
            return models;
        }

        [HttpGet("GetCurrentUserEvents")]
        [ServiceFilter(typeof(UserAccessFilter))]
        public IEnumerable<UserSpecificJamEventViewModel> GetCurrentUserEvents()
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userSpecificJamEvents = _jamEventService.GetFiltereByUser(userId);
            return userSpecificJamEvents?.Select(item => _mapper.Map<UserSpecificJamEventViewModel>(item));
        }
        [HttpGet("SearchUserEventsByName/{seacrhText}")]
        [HttpGet("SearchUserEventsByName")]
        [ServiceFilter(typeof(UserAccessFilter))]
        public IEnumerable<UserSpecificJamEventViewModel> SearchUserEventsByName(string seacrhText)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userSpecificJamEvents = _jamEventService.GetFiltereByNameForUser(seacrhText, userId);
            return userSpecificJamEvents.Select(item => _mapper.Map<UserSpecificJamEventViewModel>(item));
        }
    }
}