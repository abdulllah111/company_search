using Application.Commands.Events;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Handlers.Events
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICategoryService _categoryService;

        public DeleteEventCommandHandler(IApplicationDbContext context, ICategoryService categoryService) =>
        (_context, _categoryService) = (context, categoryService);

        public async Task<Guid> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Events.FindAsync(new object[] { request.Id }, cancellationToken);
            
            // Если событие не найдено или его автор не равен текущему пользователю, выбрасываем
            if(entity == null || entity.CreatorId != request.CreatorId) throw new NotFoundException(nameof(Event), request.Id);

            // Получаем копию CategoryIds до удаления события
            var categoryIdsToRemove = entity.CategoryIds.ToList();

            _context.Events.Remove(entity);
            
            // Обновляем флаг категорий
            foreach (var categoryId in categoryIdsToRemove)
            {
                await _categoryService.UpdateCategoryUsageStatusAsync(categoryId, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);
            
            return entity.Id;
        }
    }
}