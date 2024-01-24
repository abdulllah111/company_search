using Application.Commands.Events;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Events
{
    public class UpdateEventCommandHundler : IRequestHandler<UpdateEventCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public UpdateEventCommandHundler(IApplicationDbContext context){
            _context = context;
        }
        public async Task<Guid> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Events
            .Include(e => e.EventCategories)
            .Include(e => e.ChildEvents)
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken) ?? throw new NotImplementedException();
            


            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.StartDate = request.StartDate;
            entity.EndDate = request.EndDate;
            entity.Location = request.Location;
            entity.EventType = request.EventType;
            entity.MinParticipants = request.MinParticipants;
            entity.MaxParticipants = request.MaxParticipants;
            entity.MinAge = request.MinAge;
            entity.MaxAge = request.MaxAge;
            entity.RegistrationDeadline = request.RegistrationDeadline;
            entity.ParticipantsGender = request.ParticipantsGender;
            entity.Created = DateTime.Now;
            entity.EventCategories = new List<EventCategory>();
            
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
            
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}