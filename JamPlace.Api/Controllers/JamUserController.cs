using AutoMapper;
using JamPlace.Api.Models;
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
        [HttpGet("getUserInfo")]
        public JamUserViewModel GetUserInfo()
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _jamUserService.GetByIdentityId(userId);
            return new JamUserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName
            };
        }
    }
}
