using EventUp.Application.Dto.User;
using FluentValidation;

namespace EventUp.Api.Validation.User;

public class LoginUserValid : AbstractValidator<LoginUserDto>
{
    public LoginUserValid()
    {
        RuleFor(e => e.Name).NotEmpty().MinimumLength(4).MaximumLength(32);
        RuleFor(e => e.Password).NotEmpty().MinimumLength(6);
    }
}