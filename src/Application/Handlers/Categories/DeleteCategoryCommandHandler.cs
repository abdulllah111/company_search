using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Categories;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Handlers.Categories
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCategoryCommandHandler(IApplicationDbContext context) => _context = context;
        public async Task<Guid> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Categories.FindAsync(new object[] { request.Id }, cancellationToken);
            
            // Если категория не найдено
            // или его автор не равен текущему пользователю
            // или категория используется в других событиях
            // или есть дочерние категории - то ошибка.
            if(entity == null || 
            entity.CreatorId != request.CreatorId || 
            entity.IsShared ||
            entity.ChildCategories != null) throw new NotFoundException(nameof(Category), request.Id);

            _context.Categories.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            
            return entity.Id;
        }
    }
}