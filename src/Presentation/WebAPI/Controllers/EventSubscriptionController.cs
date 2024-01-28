using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Events;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class EventSubscriptionController : BaseController
    {
        private readonly IMapper _mapper;

        public EventSubscriptionController(IMapper mapper) => _mapper = mapper;

        [HttpPost("subscribe")]
        [Authorize]
        public async Task<IActionResult> SubscribeToEvent([FromBody] EventSubscriptionDto subscritionDto)
        {
            var command = _mapper.Map<SubscribeToEventCommand>(subscritionDto);
            command.UserId = UserId;
            
            var eventId = await Mediator!.Send(command);
            return Ok(eventId);
        }

        [HttpPost("unsubscribe")]
        [Authorize]
        public async Task<IActionResult> UnsubscribeFromEvent([FromBody] EventSubscriptionDto subscritionDto)
        {
            var command = _mapper.Map<UnsubscribeFromEventCommand>(subscritionDto);
            command.UserId = UserId;
            
            var eventId = await Mediator!.Send(command);
            return Ok(eventId);
        }
    }
}