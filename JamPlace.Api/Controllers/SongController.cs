using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JamPlace.Api.Filters;
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
    public class SongController : ControllerBase
    {
        private readonly ISongService _songService;
        private readonly IMapper _mapper;
        public SongController(ISongService songService, IMapper mapper)
        {
            _songService = songService;
            _mapper = mapper;
        }
        [HttpPost("AddSong")]
        public IActionResult AddSong(SongViewModel song)
        {
            var added = _songService.Add(_mapper.Map<ISong>(song), song.EventId);
            return Ok(_mapper.Map<SongViewModel>(added));
        }
        [HttpPost("UpdateSong")]
        public IActionResult UpdateSong(SongViewModel song)
        {
            _songService.Edit(_mapper.Map<ISong>(song), song.EventId);
            return Ok();
        }
        [HttpPost("DeleteSong")]
        public IActionResult DeleteSong(SongViewModel song)
        {
            _songService.Delete(_mapper.Map<ISong>(song));
            return Ok();
        }
        [HttpPost("SearchSongByName")]
        public IActionResult SearchUserEventsByName(SongSearchRequest request)
        {
            var songs = _songService.GetFilteredByTitle(request.SearchText, request.EventId).ToList();
            return Ok(songs.Select(item => _mapper.Map<SongViewModel>(item)));
        }
    }
}