using SPTS_Reader.EventBus;

public class ConsumerHostService : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private CancellationTokenSource _cancellationTokenSource;

    public ConsumerHostService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        Task.Run(async () =>
        {
            using var scope = _scopeFactory.CreateScope();
            var userConsumer = scope.ServiceProvider.GetRequiredService<UserConsumer>();
            var testConsumer = scope.ServiceProvider.GetRequiredService<TestConsumer>();
            await userConsumer.ConsumeMessageAsync();
            await testConsumer.ConsumeMessageAsync();
        }, _cancellationTokenSource.Token);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _cancellationTokenSource.Cancel();
        return Task.CompletedTask;
    }
}
