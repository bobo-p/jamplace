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
    public class NeededEquipmentController : ControllerBase
    {
        private readonly INeededEventEquipmentService _neededEquipmentService;
        private readonly IMapper _mapper;
        public NeededEquipmentController(INeededEventEquipmentService eqService, IMapper mapper)
        {
            _neededEquipmentService = eqService;
            _mapper = mapper;
        }
        [HttpPost("AddEquipment")]
        public ActionResult<EquipmentViewModel> AddEquipment(EquipmentViewModel comment)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var added = _neededEquipmentService.Add(_mapper.Map<IEquipment>(comment), userId);
            return Ok(_mapper.Map<EquipmentViewModel>(comment));
        }
        [HttpPost("UpdateEquipment")]
        public IActionResult UpdateEquipment(EquipmentViewModel comment)
        {
            _neededEquipmentService.Edit(_mapper.Map<IEquipment>(comment), comment.EventId);
            return Ok();
        }
        [HttpPost("DeleteEquipment")]
        public IActionResult DeleteEquipment(CommentViewModel comment)
        {
            _neededEquipmentService.Delete(_mapper.Map<IEquipment>(comment));
            return Ok();
        }
        [HttpPost("SearchEquipment")]
        public ActionResult SearchEquipment(EquipmentSearchRequest request)
        {
            var songs = _neededEquipmentService.GetFilteredByName(request.SearchText, request.EventId).ToList();
            return Ok(songs.Select(item => _mapper.Map<CommentViewModel>(item)));
        }
    }
}