using Domain.Common;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        public Guid? CreatorId { get; set; }
        public required string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
        public IList<Category>? ChildCategories { get; set; }
        public IList<Event>? CategoryEvents { get; set; }
        public bool IsShared { get; set; } 
    }
}