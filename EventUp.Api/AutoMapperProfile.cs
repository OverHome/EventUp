using AutoMapper;
using EventUp.Domain.Models;
using EventUp.Infrastructure.Dto.Event;
using EventUp.Infrastructure.Dto.EventType;
using EventUp.Infrastructure.Dto.Station;
using EventUp.Infrastructure.Dto.User;
using EventUp.Infrastructure.Tools;

namespace EventsUp;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AddEventDto, Event>();
        CreateMap<UpdateEventDto, Event>();
        CreateMap<AddEventTypeDto, EventType>();
        CreateMap<AddStationDto, Station>();
        CreateMap<RegisterUserDto, User>()
            .ForMember(e => e.PasswordHash, opt => opt.MapFrom(src => HashPasword.Hash(src.Password)));
        CreateMap<LoginUserDto, User>()
            .ForMember(e => e.PasswordHash, opt => opt.MapFrom(src => HashPasword.Hash(src.Password)));
    }
}