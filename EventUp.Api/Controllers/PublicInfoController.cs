
using AutoMapper;
using EventUp.Application.Interfaces.Services;
using EventUp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventUp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicInfoController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IStationService _stationService;
        private readonly IEventTypeService _eventTypeService;
        private readonly IMapper _mapper;

        public PublicInfoController(IEventService eventService, IStationService stationService, IEventTypeService eventTypeService, IMapper mapper)
        {
            _eventService = eventService;
            _stationService = stationService;
            _eventTypeService = eventTypeService;
            _mapper = mapper;
        }
        
        [HttpGet("Events")]
        public async Task<ActionResult<List<Event>>> GetAllEvents()
        {
            return Ok(await _eventService.GetAllEvents());
        }
        
        [HttpGet("Event")]
        public async Task<ActionResult<Event>> GetEventById([FromQuery]int id)
        {
            return Ok(await _eventService.GetEventsById(id));
        }
        
        [HttpGet("Stations")]
        public async Task<ActionResult<List<Station>>> GetAllStation()
        {
            return Ok(await _stationService.GetAllStations());
        }
        
        [HttpGet("Station")]
        public async Task<ActionResult<Station>> GetStationById([FromQuery]int id)
        {
            return Ok(await _stationService.GetStationById(id));
        }
        
        [HttpGet("EventTypes")]
        public async Task<ActionResult<List<EventType>>> GetAllEventType()
        {
            return Ok(await _eventTypeService.GetAllEventType());
        }
        
        [HttpGet("EventType")]
        public async Task<ActionResult<EventType>> GetEventTypeById([FromQuery]int id)
        {
            return Ok(await _eventTypeService.GetEventTypeById(id));
        }
        
        [HttpGet("Events/Filter")]
        public async Task<ActionResult<Event>> GetEventByFilter([FromQuery]EventFilter filter)
        {
            return Ok(await _eventService.GetEventByFilter(filter));
        }
    }
}
