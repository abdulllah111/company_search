using Domain.Common;

namespace Domain.Entities
{
    public class EventCategory : BaseEntity
    {
        public Guid EventId { get; set; }
        public Event? Event { get; set; }
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
        
    }
}