using AutoMapper;
using JamPlace.Api.Models;
using JamPlace.DomainLayer.Interfaces.Models;
using JamPlace.DomainLayer.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JamPlace.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JamUserController : ControllerBase
    {
        private readonly IJamUserService _jamUserService;
        private readonly IMapper _mapper;
        public JamUserController(IJamUserService jamUserService, IMapper mapper)
        {
            _jamUserService = jamUserService;
            _mapper = mapper;
        }
        [HttpGet("GetLoggedUserInfo")]
        public JamUserViewModel GetUserInfo()
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string email = HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var user = _jamUserService.GetByIdentityId(userId,email);
            return new JamUserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                PhotoBase64 = user.PhotoBase64
            };
        }
        [HttpPost("SaveUserData")]
        public IActionResult SaveUserData(JamUserViewModel userData)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var domainModel = _mapper.Map<IJamUser>(userData);
            domainModel.UserIdentityId = userId;
            _jamUserService.Edit(domainModel);
            return Ok();
        }
    }
}
