using EventUp.Application.Dto.Event;
using FluentValidation;

namespace EventUp.Api.Validation.Event;

public class UpdateEventValid : AbstractValidator<UpdateEventDto>
{
    public UpdateEventValid()
    {
        RuleFor(e=> e.Title).NotEmpty().MaximumLength(50);
        RuleFor(e=> e.About).NotEmpty().MaximumLength(1000);
        RuleFor(e => e.StartDuring).NotEmpty();
        RuleFor(e => e.EndDuring).NotEmpty();
    }
}