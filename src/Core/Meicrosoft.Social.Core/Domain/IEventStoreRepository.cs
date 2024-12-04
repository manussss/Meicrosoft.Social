namespace Meicrosoft.Social.Core.Domain;

public interface IEventStoreRepository
{
    Task SaveAsync(EventModel @event);
    Task<List<EventModel>> FindByAggregateIdAsync(Guid aggregateId);
}
