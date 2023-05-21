using System.Reflection;
using AutoMapper;
using EventUp.Application.Dto.Event;
using EventUp.Application.Dto.EventType;
using EventUp.Application.Dto.Station;
using EventUp.Application.Interfaces.Services;
using EventUp.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace EventUp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = nameof(UserRoles.Admin))]
    public class AdminPanelController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IStationService _stationService;
        private readonly IEventTypeService _eventTypeService;
        private readonly IMapper _mapper;

        private string infoControllerName = "PublicInfo";

        public AdminPanelController(IEventService eventService, IStationService stationService,
            IEventTypeService eventTypeService, IMapper mapper)
        {
            _eventService = eventService;
            _stationService = stationService;
            _eventTypeService = eventTypeService;
            _mapper = mapper;
        }

        private CreatedAtActionResult responseOnCreate(string endPoint, object createdResource)
        {
            var id = (int)createdResource.GetType().GetProperty("Id")?.GetValue(createdResource)!;
            return CreatedAtAction(endPoint, infoControllerName, new { id = id }, createdResource);
        }

        [HttpPost("Event")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Event>> AddEvent([FromBody] AddEventDto newEvent)
        {
            var endPoint = nameof(PublicInfoController.GetEventById);
            var createdResource = await _eventService.AddEvent(_mapper.Map<Event>(newEvent));
            return responseOnCreate(endPoint, createdResource);
        }

        [HttpPut("Event")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateEvent([FromBody] UpdateEventDto updateEvent)
        {
            var a = await _eventService.UpdateEvent(_mapper.Map<Event>(updateEvent));
            return NoContent();
        }

        [HttpDelete("Event")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteEvent([FromQuery] int id)
        {
            await _eventService.DeleteEventById(id);
            return NoContent();
        }

        [HttpPost("Station")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Station>> AddStation([FromBody] AddStationDto newEvent)
        {
            var endPoint = nameof(PublicInfoController.GetStationById);
            var createdResource = await _stationService.AddStations(_mapper.Map<Station>(newEvent));
            return responseOnCreate(endPoint, createdResource);
        }

        [HttpDelete("Station")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteStation([FromQuery] int id)
        {
            await _stationService.DeleteStationsById(id);
            return NoContent();
        }

        [HttpPost("EventType")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<EventType>> AddEventType([FromBody] AddEventTypeDto newEventType)
        {
            var endPoint = nameof(PublicInfoController.GetEventTypeById);
            var createdResource = await _eventTypeService.AddEventType(_mapper.Map<EventType>(newEventType));
            return responseOnCreate(endPoint, createdResource);
        }


        [HttpDelete("EventType")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteEventType([FromQuery] int id)
        {
            await _eventTypeService.DeleteEventTypeById(id);
            return NoContent();
        }
    }
}