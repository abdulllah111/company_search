using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Queries.Events.GetEvents
{
    public class EventDto : IMapFrom<Event>
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int MinParticipants { get; set; }
        public int MaxParticipants { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime RegistrationDeadline { get; set; }
        public IList<EventCategory>? EventCategories { get; set; }
        public int MembersCount { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Event, EventDto>()
            .ForMember(eventDto => eventDto.MembersCount,
            opt => opt.MapFrom(eventEntity => eventEntity.Members!.Count));
        }
    }
}