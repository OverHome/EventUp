using AutoMapper;
using EventUp.Application.Dto.Event;
using EventUp.Application.Dto.EventType;
using EventUp.Application.Dto.Station;
using EventUp.Application.Dto.User;
using EventUp.Domain.Models;
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