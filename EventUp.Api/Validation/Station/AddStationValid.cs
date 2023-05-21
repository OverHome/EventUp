using EventUp.Application.Dto.Station;
using FluentValidation;

namespace EventUp.Api.Validation.Station;

public class AddStationValid : AbstractValidator<AddStationDto>
{
    public AddStationValid()
    {
        RuleFor(e => e.PlaceName).NotEmpty().MaximumLength(200);
        RuleFor(e => e.PlaceAddress).NotEmpty().MaximumLength(200);
        RuleFor(e => e.GeoLat).NotEmpty();
        RuleFor(e => e.GeoLong).NotEmpty();

    }
}