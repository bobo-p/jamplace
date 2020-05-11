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
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }
        [HttpPost("AddComment")]
        public ActionResult<CommentViewModel> AddComment(CommentViewModel comment)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var added = _commentService.Add(_mapper.Map<IComment>(comment), userId);
            return Ok(_mapper.Map<CommentViewModel>(added));
        }
        [HttpPost("UpdateComment")]
        public IActionResult UpdateComment(CommentViewModel comment)
        {
            _commentService.Edit(_mapper.Map<IComment>(comment), comment.EventId);
            return Ok();
        }
        [HttpPost("DeleteComment")]
        public IActionResult DeleteComment(CommentViewModel comment)
        {
            _commentService.Delete(_mapper.Map<IComment>(comment));
            return Ok();
        }
        [HttpPost("SearchComment")]
        public ActionResult SearchComment(CommentSearchRequest request)
        {
            var songs = _commentService.GetFilteredByUserName(request.SearchText, request.EventId).ToList();
            return Ok(songs.Select(item => _mapper.Map<CommentViewModel>(item)));
        }

    }
}