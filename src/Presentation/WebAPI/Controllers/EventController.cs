using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Events;
using Application.Queries.Events.GetEvent;
using Application.Queries.Events.GetEvents;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class EventController : BaseController
    {
        private readonly IMapper _mapper;

        public EventController(IMapper mapper) => _mapper = mapper;

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<EventsVm>> GetAll(){
            var query = new GetEventsQuery(){
                UserId = UserId
            };
            var vm = await Mediator!.Send(query);

            return Ok(vm);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<EventDetailsVm>> Get(Guid id){
            var query = new GetEventDetailsQuery{
                Id = id,
                UserId = UserId
            };
            var vm = await Mediator!.Send(query);

            return Ok(vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CreateEventCommand>> Post([FromBody] CreateEventDto createEventDto){
            var command = _mapper.Map<CreateEventCommand>(createEventDto);
            command.CreatorId = UserId;
            var eventId = await Mediator!.Send(command);

            return Ok(eventId);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Put([FromBody] UpdateEventDto updateEventDto){
            var command = _mapper.Map<UpdateEventCommand>(updateEventDto);
            command.CreatorId = UserId;
            await Mediator!.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id){
            var command = new DeleteEventCommand{
                Id = id,
                CreatorId = UserId
            };

            await Mediator!.Send(command);
            return NoContent();
        }
    }
}