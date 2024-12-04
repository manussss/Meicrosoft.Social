namespace Meicrosoft.Social.Command.Infra.Handlers;

public class EventSourcingHandler(IEventStore eventStore) : IEventSourcingHandler<PostAggregate>
{
    public async Task<PostAggregate> GetByIdAsync(Guid aggregateId)
    {
        var aggregate = new PostAggregate();
        var events = await eventStore.GetEventsAsync(aggregateId);

        if (events == null || !events.Any())
            return aggregate;

        aggregate.ReplayEvents(events);
        aggregate.Version = events.Select(x => x.Version).Max();

        return aggregate;
    }

    public async Task SaveAsync(AggregateRoot aggregate)
    {
        await eventStore.SaveEventAsync(aggregate.Id, aggregate.GetUncomittedChanges(), aggregate.Version);
        aggregate.MarkChangesAsCommitted();
    }
}
