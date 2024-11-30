namespace Meicrosoft.Social.Core.Domain;

public abstract class AggregateRoot
{
    protected Guid _id;
    private readonly List<BaseEvent> _changes = [];

    public Guid Id
    {
        get { return _id; }
    }

    public int Version { get; set; } = -1;

    public IEnumerable<BaseEvent> GetUncomittedChanges()
    {
        return _changes;
    }

    public void MarkChangesAsCommitted()
    {
        _changes.Clear();
    }

    public void ReplayEvents(IEnumerable<BaseEvent> events)
    {
        foreach (var @event in events)
        {
            ApplyChange(@event, false);
        }
    }

    protected void RaiseEvent(BaseEvent @event)
    {
        ApplyChange(@event, true);
    }

    private void ApplyChange(BaseEvent @event, bool isNew)
    {
        var method = this.GetType().GetMethod("Apply", [@event.GetType()]);

        if (method == null)
            throw new ArgumentNullException(nameof(method), $"The 'Apply' method was not found in the aggregate for {@event.GetType().Name}");

        method.Invoke(this, new object[] { @event });

        if (isNew)
        {
            _changes.Add(@event);
        }
    }
}
