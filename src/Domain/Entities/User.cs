using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }   
        public int Age { get; set; }
        public required Gender Gender { get; set; }
        public IList<Event>? CreatedEvents { get; set; }
        public IList<EventMember>? EventMemberships { get; set; }
    }
}