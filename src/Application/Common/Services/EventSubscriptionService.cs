using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Services
{
    public class EventSubscriptionService : IEventSubscriptionService
    {
        private readonly IApplicationDbContext _context;

        public EventSubscriptionService(IApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task SubscribeUserAsync(Guid eventId, Guid userId, CancellationToken cancellationToken)
        {
            // Проверка на существование подписки
            var existingSubscription = await _context.EventMembers
            .FirstOrDefaultAsync(em => em.EventId == eventId && em.UserId == userId, cancellationToken);
            
            // Проверка на существование события
            var eventEntity = await _context.Events
            .FirstOrDefaultAsync(e => e.Id == eventId, cancellationToken);

            // Проверка на существование пользователя
            var memberEntity = await _context.Users
            .FindAsync(new object[] { userId }, cancellationToken);


            // Событие и пользователь не найдены / уже зарегестрирован - ошибка
            if (eventEntity == null && memberEntity == null && existingSubscription != null) throw new NotFoundException(nameof(Event), eventId);

            // Событие завершено и регистрация закрыта - ошибка
            if (eventEntity!.EndDate.HasValue && eventEntity.EndDate < DateTime.Now && eventEntity.RegistrationDeadline < DateTime.Now)
            {
                throw new NotFoundException("Event ended");
            }

            // Достигнуто максимальное количество участников - ошибка
            if (eventEntity.MaxParticipants > 0 && eventEntity.Members!.Count >= eventEntity.MaxParticipants)
            {
                throw new NotFoundException("Max participants");
            }

            // Недопустимый возраст - ошибка
            if(eventEntity.MinAge > memberEntity!.Age && eventEntity.MaxAge < memberEntity.Age)
            {
                throw new NotFoundException("Inappropriate age");
            }

            // Недопустимый пол - ошибка
            if(eventEntity.ParticipantsGender != Gender.Any && eventEntity.ParticipantsGender != memberEntity.Gender)
            {
                throw new NotFoundException("Inappropriate gender");
            }

            // Подиска от создателя - ошибка
            if(eventEntity.CreatorId == memberEntity.Id)
            {
                throw new NotFoundException("You are the event creator");
            }

            var newSubscription = new EventMember 
            { 
                Id = new Guid(),
                EventId = eventId, 
                UserId = userId,
                Created = DateTime.Now
            };
            _context.EventMembers.Add(newSubscription);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UnsubscribeUserAsync(Guid eventId, Guid userId, CancellationToken cancellationToken)
        {
            var subscriptionToRemove = await _context.EventMembers
            .FirstOrDefaultAsync(em => em.EventId == eventId && em.UserId == userId, cancellationToken);

            if (subscriptionToRemove != null)
            {
                _context.EventMembers.Remove(subscriptionToRemove);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}