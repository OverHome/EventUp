using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventUp.Application.Interfaces.Services;
using EventUp.Domain.Models;
using EventUp.Infrastructure.Dto;
using EventUp.Infrastructure.Dto.Event;
using EventUp.Infrastructure.Dto.Station;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventUp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IStationService _stationService;
        private readonly IMapper _mapper;

        public EventsController(IEventService eventService, IStationService stationService, IMapper mapper)
        {
            _eventService = eventService;
            _stationService = stationService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Event>>> GetAllEvents()
        {
            return Ok(await _eventService.GetAllEvents());
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Event>>> GetEventById([FromQuery]int id)
        {
            return Ok(await _eventService.GetEventsById(id));
        }
        
        [HttpPost]
        public async Task<ActionResult<List<Event>>> AddEvent([FromBody]AddEventDto newEvent)
        {
            return Ok(await _eventService.AddEvent(_mapper.Map<Event>(newEvent)));
        }
        [HttpDelete]
        public async Task<ActionResult<List<Event>>> DeleteEventById([FromQuery]int id)
        {
            return Ok(await _eventService.DeleteEventById(id));
        }
        
        [HttpGet("GetAllStation")]
        public async Task<ActionResult<List<Station>>> GetAllStation()
        {
            return Ok(await _stationService.GetAllStations());
        }
        
        [HttpPost("Station")]
        public async Task<ActionResult<List<Station>>> Station([FromBody]AddStationDto newStation)
        {
            return Ok(await _stationService.AddStations(_mapper.Map<Station>(newStation)));
        }
    }
}
