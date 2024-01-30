using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IApplicationDbContext _context;

        public CategoryService(IApplicationDbContext context) => _context = context;
        public async Task UpdateCategoryUsageStatusAsync(Guid categoryId, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .Include(c => c.CategoryEvents)
                .FirstOrDefaultAsync(c => c.Id == categoryId, cancellationToken);

            if (category != null)
            {
                bool isShared = category.CategoryEvents?.Any() ?? false;  // Проверяем, есть ли использование категории

                if (isShared != category.IsShared)
                {
                    category.IsShared = isShared;
                    
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }
        }

    }
}