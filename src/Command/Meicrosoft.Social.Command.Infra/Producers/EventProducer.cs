namespace Meicrosoft.Social.Command.Infra.Producers;

public class EventProducer(IOptions<ProducerConfig> config) : IEventProducer
{
    public async Task ProduceAsync<T>(string topic, T @event) where T : BaseEvent
    {
        using var producer = new ProducerBuilder<string, string>(config.Value)
            .SetKeySerializer(Serializers.Utf8)
            .SetValueSerializer(Serializers.Utf8)
            .Build();

        var eventMessage = new Message<string, string>
        {
            Key = Guid.NewGuid().ToString(),
            Value = JsonSerializer.Serialize(@event, @event.GetType())
        };

        var deliveryResult = await producer.ProduceAsync(topic, @eventMessage);

        if (deliveryResult.Status == PersistenceStatus.NotPersisted)
            throw new Exception($"could not produce '{@event.GetType().Name}' message to topic '{topic}' due to the following reason '{deliveryResult.Message}'");
    }
} 
