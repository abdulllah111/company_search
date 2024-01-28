using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;

namespace Application.Queries.Events.GetEvent
{
    public class EventDetailsVm : IMapFrom<Event>
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public LocationPoint? Location { get; set; }
        public required EventType EventType { get; set; }
        public int MinParticipants { get; set; }
        public int MaxParticipants { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public DateTime RegistrationDeadline { get; set; }
        public Gender ParticipantsGender { get; set; }
        public IList<EventCategory>? EventCategories { get; set; }
        public int MembersCount { get; set; }
        public IList<Event>? ChildEvents { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Event, EventDetailsVm>()
            .ForMember(eventVm => eventVm.MembersCount,
            opt => opt.MapFrom(eventEntity => eventEntity.Members!.Count));
        }
    }
}