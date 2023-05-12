using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EventUp.Api.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        [HttpGet("login")]
        public ActionResult Login([FromQuery]string username)
        {
            var claims = new List<Claim> {new Claim(ClaimTypes.Name, username) };
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(60)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            
            return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
        }
    }
}
