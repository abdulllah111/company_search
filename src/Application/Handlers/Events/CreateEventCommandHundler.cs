using Application.Commands.Events;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Events
{
    public class CreateEventCommandHundler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateEventCommandHundler(IApplicationDbContext context){
            _context = context;
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
                EventCategories = new List<EventCategory>(),
            };

            // Обработка категорий
            if (request.CategoryIds != null && request.CategoryIds.Any())
            {
                // Получаем категории из базы данных по идентификаторам
                var selectedCategories = await _context.Categories
                    .Where(c => request.CategoryIds.Contains(c.Id))
                    .ToListAsync(cancellationToken);

                // Создаем связи между событием и категориями
                entity.EventCategories.ToList().AddRange(selectedCategories.Select(category => new 
                EventCategory {Id = new Guid(), EventId = entity.Id, CategoryId = category.Id, Created = DateTime.Now}));
            }
            
            if (request.ParentEventId.HasValue)
            {
                var parentEvent = await _context.Events.FindAsync(request.ParentEventId.Value, cancellationToken);
                entity.ParentEvent = parentEvent;
            }

            await _context.Events.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}