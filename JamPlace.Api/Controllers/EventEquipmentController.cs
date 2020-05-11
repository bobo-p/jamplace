using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using JamPlace.Api.Models;
using JamPlace.DomainLayer.Interfaces.Models;
using JamPlace.DomainLayer.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JamPlace.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventEquipmentController : ControllerBase
    {
        private readonly IEventEquipmentService _eventEquipmentService;
        private readonly IMapper _mapper;
        public EventEquipmentController(IEventEquipmentService eqService, IMapper mapper)
        {
            _eventEquipmentService = eqService;
            _mapper = mapper;
        }
        [HttpPost("AddEquipment")]
        public ActionResult<EquipmentViewModel> AddEquipment(EquipmentViewModel comment)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var added = _eventEquipmentService.Add(_mapper.Map<IEquipment>(comment), userId);
            return Ok(_mapper.Map<EquipmentViewModel>(added));
        }
        [HttpPost("UpdateEquipment")]
        public IActionResult UpdateEquipment(EquipmentViewModel comment)
        {
            _eventEquipmentService.Edit(_mapper.Map<IEquipment>(comment), comment.EventId);
            return Ok();
        }
        [HttpPost("DeleteEquipment")]
        public IActionResult DeleteEquipment(EquipmentViewModel comment)
        {
            _eventEquipmentService.Delete(_mapper.Map<IEquipment>(comment));
            return Ok();
        }
        [HttpPost("SearchEquipment")]
        public ActionResult SearchEquipment(EquipmentSearchRequest request)
        {
            var songs = _eventEquipmentService.GetFilteredByName(request.SearchText, request.EventId).ToList();
            return Ok(songs.Select(item => _mapper.Map<EquipmentViewModel>(item)));
        }
    }
}