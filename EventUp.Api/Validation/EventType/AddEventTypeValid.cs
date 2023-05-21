using EventUp.Application.Dto.EventType;
using FluentValidation;

namespace EventUp.Api.Validation.EventType;

public class AddEventTypeValid : AbstractValidator<AddEventTypeDto>
{
    public AddEventTypeValid()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(20);
        RuleFor(e => e.Code).NotEmpty().MaximumLength(20);
    }    
}