using AutoMapper;
using EventUp.Domain.Models;
using EventUp.Infrastructure.Dto.Event;
using EventUp.Infrastructure.Dto.EventType;
using EventUp.Infrastructure.Dto.Station;

namespace EventsUp;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AddEventDto, Event>();
        CreateMap<AddStationDto, Station>();
        CreateMap<AddEventTypeDto, EventType>();
    }
}