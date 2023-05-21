using EventUp.Application.Dto.Event;
using FluentValidation;

namespace EventUp.Api.Validation.Event;

public class AddEventValid : AbstractValidator<AddEventDto>
{
    public AddEventValid()
    {
        RuleFor(e=> e.Title).NotEmpty().MaximumLength(200);
        RuleFor(e=> e.About).NotEmpty().MaximumLength(1000);
        RuleFor(e => e.StartDuring).NotEmpty();
        RuleFor(e => e.EndDuring).NotEmpty();
    }
}