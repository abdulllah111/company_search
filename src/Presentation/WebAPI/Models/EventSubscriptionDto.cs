using Application.Commands.Events;
using Application.Common.Mappings;

namespace WebAPI.Models
{
    public class EventSubscriptionDto : IMapFrom<SubscribeToEventCommand>, IMapFrom<UnsubscribeFromEventCommand>
    {
        public Guid EventId { get; set; }
    }
}
