using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EventUp.Application.Interfaces.Services;
using EventUp.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventUp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("FavoriteEvents")]
        public async Task<ActionResult<List<Event>>> GetUserFavoriteEvents()
        {
            var identity = User.Identity as ClaimsIdentity;
            var nameIdentifier = Convert.ToInt32(identity!.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return Ok(await _userService.GetUserFavoriteEvents(nameIdentifier));
        }
        [HttpPost("FavoriteEvent")]
        public async Task<ActionResult<Event>> AddUserFavoriteEvent([FromBody]int eventId)
        {
            var identity = User.Identity as ClaimsIdentity;
            var nameIdentifier = Convert.ToInt32(identity!.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return Ok(await _userService.AddUserFavoriteEvents(nameIdentifier, eventId));
        }
    }
}
