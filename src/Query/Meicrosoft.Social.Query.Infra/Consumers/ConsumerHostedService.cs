namespace Meicrosoft.Social.Query.Infra.Consumers;

public class ConsumerHostedService(
    ILogger<ConsumerHostedService> logger,
    IServiceProvider serviceProvider) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("{Class} | Event consumer service running", nameof(ConsumerHostedService));

        using (IServiceScope scope = serviceProvider.CreateScope())
        {
            var eventConsumer = scope.ServiceProvider.GetRequiredService<IEventConsumer>();
            var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");

            Task.Run(() => eventConsumer.Consume(topic!), cancellationToken);
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("{Class} | Event consumer service stopped", nameof(ConsumerHostedService));

        return Task.CompletedTask;
    }
}
