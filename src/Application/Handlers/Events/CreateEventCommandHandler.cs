using Application.Commands.Events;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Events
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        private readonly ICategoryService _categoryService;
        public CreateEventCommandHandler(IApplicationDbContext context, ICategoryService categoryService){
            _context = context;
            _categoryService = categoryService;
        }
        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var entity = new Event
            {
                Id = new Guid(),
                Name = request.Name, 
                Description = request.Description, 
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Location = request.Location, 
                EventType = request.EventType,
                MinParticipants = request.MinParticipants,
                MaxParticipants = request.MaxParticipants,
                MinAge = request.MinAge,
                MaxAge = request.MaxAge,
                RegistrationDeadline = request.RegistrationDeadline,
                ParticipantsGender = request.ParticipantsGender,
                CreatorId = request.CreatorId,
                Created = DateTime.Now,
                LastModified = DateTime.UtcNow,
                CategoryIds = request.CategoryIds,
                ParentEventId = request.ParentEventId
            };

            // Устанавливаем необходимый флаг для добавленных категорий
            foreach (var categoryId in request.CategoryIds)
            {
                await _categoryService.UpdateCategoryUsageStatusAsync(categoryId, cancellationToken);
            }

            await _context.Events.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}