namespace Meicrosoft.Social.Core.Consumers;

public interface IEventConsumer
{
    void Consume(string topic);
}
