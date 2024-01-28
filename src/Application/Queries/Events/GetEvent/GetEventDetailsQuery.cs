using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Application.Queries.Events.GetEvent
{
    public class GetEventDetailsQuery : IRequest<EventDetailsVm> 
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}