using Application.Commands.Events;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Services;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Events
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICategoryService _categoryService;
        public UpdateEventCommandHandler(IApplicationDbContext context, ICategoryService categoryService) =>
        (_context, _categoryService) = (context, categoryService);
        public async Task<Guid> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Events
            .FindAsync(new object[] { request.Id }, cancellationToken);
            

            // Если событие не найдено или его автор не равен текущему пользователю, выбрасываем
            if(entity == null || entity.CreatorId != request.CreatorId) throw new NotFoundException(nameof(Event), request.Id);
            
            // Получаем удаленные из события категории
            var removedCategories = entity.EventCategories!
            .Except(request.EventCategories!)
            .ToList();

            entity.LastModified = DateTime.Now;
            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.StartDate = request.StartDate.ToUniversalTime();
            entity.EndDate = request.EndDate!.Value.ToUniversalTime();
            entity.Location = request.Location;
            entity.EventType = request.EventType;
            entity.MinParticipants = request.MinParticipants;
            entity.MaxParticipants = request.MaxParticipants;
            entity.MinAge = request.MinAge;
            entity.MaxAge = request.MaxAge;
            entity.RegistrationDeadline = request.RegistrationDeadline.ToUniversalTime();
            entity.ParticipantsGender = request.ParticipantsGender;
            entity.Created = DateTime.Now;
            entity.EventCategories = request.EventCategories;
            

            // Обновляем флаги для удаленных из события категорий 
            foreach (var removedCategory in removedCategories)
            {
               await _categoryService.UpdateCategoryUsageStatusAsync(removedCategory.Id, cancellationToken);
            }

            // Проверяем новые категории добавленные к событию и обновляем флаги категорий
            if (request.EventCategories != null && request.EventCategories.Any())
            {
                // Устанавливаем необходимый флаг для добавленных категорий
                foreach (var category in request.EventCategories)
                {
                    await _categoryService.UpdateCategoryUsageStatusAsync(category.Id, cancellationToken);
                }
            
            }

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}