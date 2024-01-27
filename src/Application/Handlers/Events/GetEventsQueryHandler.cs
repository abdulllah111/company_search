using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Queries.Events.GetEvents;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Events
{
    public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, EventsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEventsQueryHandler(IApplicationDbContext context, IMapper mapper) =>
        (_context, _mapper) = (context, mapper);

        public async Task<EventsVm> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            var events = await _context.Events
                .Where(eventEntity => 
                eventEntity.EndDate >= DateTime.Now && 
                eventEntity.MaxParticipants >= eventEntity.MemberIds!.Count && 
                eventEntity.RegistrationDeadline >= DateTime.Now)
                .ProjectTo<EventDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken) ;

            return new EventsVm { Events = events};
        }
    }
}