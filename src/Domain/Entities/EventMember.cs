using Domain.Common;

namespace Domain.Entities
{
    public class EventMember : BaseEntity
    {
        public Guid EventId { get; set; }
        public Event? Event { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        
    }
}