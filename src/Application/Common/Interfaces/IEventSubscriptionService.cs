namespace Application.Common.Interfaces
{
    public interface IEventSubscriptionService
    {
        Task SubscribeUserAsync(Guid eventId, Guid userId, CancellationToken cancellationToken);
        Task UnsubscribeUserAsync(Guid eventId, Guid userId, CancellationToken cancellationToken);
    }
}