using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Events;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Handlers.Events
{
    public class UnsubscribeFromEventCommandHandler : IRequestHandler<UnsubscribeFromEventCommand, Guid>
    {
        private readonly IEventSubscriptionService _subscriptionService;

        public UnsubscribeFromEventCommandHandler(IEventSubscriptionService subscriptionService) =>
        _subscriptionService = subscriptionService;

        public async Task<Guid> Handle(UnsubscribeFromEventCommand request, CancellationToken cancellationToken)
        {
            await _subscriptionService.UnsubscribeUserAsync(request.EventId, request.UserId, cancellationToken);
            return request.EventId;
        }
    }
}