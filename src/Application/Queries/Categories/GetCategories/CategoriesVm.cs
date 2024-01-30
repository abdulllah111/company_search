using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Queries.Categories.GetCategories
{
    public class CategoriesVm
    {
        public IList<CategoryDto>? Categories { get; set; }
    }
}