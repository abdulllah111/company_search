using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Queries.Categories.GetCategories
{
    public class CategoryDto : IMapFrom<Category>
    {
        public Guid? CreatorId { get; set; }
        public required string Name { get; set; }
        public IList<Category>? ChildCategories { get; set; }
        public bool IsShared { get; set; } 
    }
}