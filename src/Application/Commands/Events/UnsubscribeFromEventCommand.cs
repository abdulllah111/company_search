using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Application.Commands.Events
{
    public class UnsubscribeFromEventCommand : IRequest<Guid>
    {
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
    }
}