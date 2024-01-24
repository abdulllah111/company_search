using Domain.Common;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        public required string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
        public ICollection<EventCategory>? EventCategories { get; set; }
    }
}