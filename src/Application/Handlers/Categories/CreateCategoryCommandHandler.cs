using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Categories;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Handlers.Categories
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateCategoryCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            // Проверка на дублирование по имени
            if (_context.Categories.Any(c => c.Name == request.Name))
            {
                throw new InvalidOperationException("Категория с таким именем уже существует.");
            }

            var entity = new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CreatorId = request.CreatorId,
                Created = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                ParentCategoryId = request.ParentCategoryId
            };

            await _context.Categories.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}