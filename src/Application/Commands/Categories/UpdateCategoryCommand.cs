using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Commands.Categories
{
    public class UpdateCategoryCommand
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}