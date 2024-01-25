using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Events;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Handlers.Events
{
    public class SubscribeToEventCommandHandler : IRequestHandler<SubscribeToEventCommand, Guid>
    {
        private readonly IEventSubscriptionService _subscriptionService;

        public SubscribeToEventCommandHandler(IEventSubscriptionService subscriptionService) => 
        _subscriptionService = subscriptionService;
        public async Task<Guid> Handle(SubscribeToEventCommand request, CancellationToken cancellationToken)
        {
            await _subscriptionService.SubscribeUserAsync(request.EventId, request.UserId, cancellationToken);
            return request.EventId;
        }
    }
}