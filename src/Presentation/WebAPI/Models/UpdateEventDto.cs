using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Events;
using Application.Common.Mappings;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;

namespace WebAPI.Models
{
    public class UpdateEventDto : IMapFrom<UpdateEventCommand>
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
        public IList<Category>? EventCategories { get; set; }
        public Guid? ParentEventId { get; set; }
        
    }
}