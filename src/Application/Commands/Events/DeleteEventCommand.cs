using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Application.Commands.Events
{
    public class DeleteEventCommand : IRequest<Guid> 
    {
        public Guid Id { get; set; }
        public Guid CreatorId { get; set; }
    }
}