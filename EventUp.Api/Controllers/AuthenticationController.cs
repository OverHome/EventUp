using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EventUp.Application.Dto.User;
using EventUp.Application.Interfaces.Services;
using EventUp.Domain.Models;
using EventUp.Infrastructure.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Exception = System.Exception;

namespace EventUp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;

        public AuthenticationController(IUserService userService, IJwtTokenService jwtTokenService, IMapper mapper)
        {
            _userService = userService;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
        }
        [HttpPost("register")]
        public async Task<ActionResult<JwtToken>> Register([FromBody] RegisterUserDto newUser)
        {
            var userInBd = await _userService.GetUser(newUser.Name);
            if (userInBd is not null)
            {
                throw new Exception("UserExist");
            }
            return Ok(await _userService.AddUser(_mapper.Map<User>(newUser)));
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<JwtToken>> Login([FromBody] LoginUserDto user)
        {
            var userInBd = await _userService.GetUser(user.Name) ?? throw new Exception("UserNotFound");
            if (userInBd.PasswordHash != HashPasword.Hash(user.Password))
            {
                throw new Exception("BadPassword");
            }
            return Ok(await _jwtTokenService.CreateJwtToken(userInBd));
        }
    }
}