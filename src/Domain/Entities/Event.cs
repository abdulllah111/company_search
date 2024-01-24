using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Event : BaseEntity
    {
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

        public Guid CreatorId { get; set; }
        public User? Creator { get; set; }
        
        public required ICollection<EventCategory> EventCategories { get; set; }
        public ICollection<EventMember>? Members { get; set; }

        public Guid? ParentEventId { get; set; }
        public Event? ParentEvent { get; set; }
        public ICollection<Event>? ChildEvents { get; set; }
    }
}