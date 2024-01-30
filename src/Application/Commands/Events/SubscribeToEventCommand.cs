using Domain.Enums;
using MediatR;

namespace Application.Commands.Events
{
    public class SubscribeToEventCommand : IRequest<Guid>
    {
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        public int UserAge { get; set; }
        public Gender? UserGender { get; set; }

    }
}