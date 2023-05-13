using EventUp.Domain.Models;

namespace EventUp.Application.Interfaces.Services;

public interface IJwtTokenService
{
    Task<JwtToken> CreateJwtToken(User user);
}