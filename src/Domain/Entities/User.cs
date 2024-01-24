using Domain.Common;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }   

        public ICollection<Event>? CreatedEvents { get; set; }

        public ICollection<EventMember>? EventMemberships { get; set; }
    }
}