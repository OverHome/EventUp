using EventUp.Application.Dto.User;
using FluentValidation;

namespace EventUp.Api.Validation.User;

public class AddUserValid : AbstractValidator<RegisterUserDto>
{
    public AddUserValid()
    {
        RuleFor(e => e.Name).NotEmpty().MinimumLength(4).MaximumLength(32);
        RuleFor(e => e.Password).NotEmpty().MinimumLength(6);
        RuleFor(e => e.RepeatPassword).NotEmpty().Must((model, property2) => property2 == model.Password).WithMessage("Wrong passwords");
    }
}