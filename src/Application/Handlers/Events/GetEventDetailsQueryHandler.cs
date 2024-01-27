using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Queries.Events.GetEvent;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Events
{
    public class GetEventDetailsQueryHandler : IRequestHandler<GetEventDetailsQuery, EventDetailsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEventDetailsQueryHandler(IApplicationDbContext context, IMapper mapper) =>
        (_context, _mapper) = (context, mapper);

        public async Task<EventDetailsVm> Handle(GetEventDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Events.FirstOrDefaultAsync(eventEntity =>
            eventEntity.Id == request.Id, cancellationToken) ?? 
            throw new NotFoundException(nameof(Event), request.Id); 
        
            return _mapper.Map<EventDetailsVm>(entity);
        }
    }
}