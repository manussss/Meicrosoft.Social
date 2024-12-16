namespace Meicrosoft.Social.Query.Infra.Consumers;

public class EventConsumer(
    IOptions<ConsumerConfig> consumerConfig, 
    IEventHandler eventHandler) : IEventConsumer
{
    public void Consume(string topic)
    {
        using var consumer = new ConsumerBuilder<string, string>(consumerConfig.Value)
            .SetKeyDeserializer(Deserializers.Utf8)
            .SetValueDeserializer(Deserializers.Utf8)
            .Build();

        consumer.Subscribe(topic);

        while (true)
        {
            var consumerResult = consumer.Consume();

            if (consumerResult?.Message == null) continue;

            var options = new JsonSerializerOptions { Converters = { new EventJsonConverter() } };
            var @event = JsonSerializer.Deserialize<BaseEvent>(consumerResult.Message.Value, options);
            var handlerMethod = eventHandler.GetType().GetMethod("On", new Type[] { @event.GetType() });

            if (handlerMethod == null)
                throw new ArgumentNullException(nameof(handlerMethod), "could not find event handler method");

            handlerMethod.Invoke(eventHandler, [@event]);
            consumer.Commit(consumerResult);
        }
    }
}
