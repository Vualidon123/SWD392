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
            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var userConsumer = scope.ServiceProvider.GetRequiredService<UserConsumer>();
                var testConsumer = scope.ServiceProvider.GetRequiredService<TestConsumer>();
                var QuestionConsumer = scope.ServiceProvider.GetRequiredService<QuestionConsumer>();
                var SchoolConsumer = scope.ServiceProvider.GetRequiredService<SchoolConsumer>();
                var HistoryConsumer = scope.ServiceProvider.GetRequiredService<HistoryConsumer>();
                // Run both consumers in parallel and wait for both to finish
                var userTask = userConsumer.ConsumeMessageAsync(cancellationToken);
                var testTask = testConsumer.ConsumeMessageAsync(cancellationToken);
                var questionTask = QuestionConsumer.ConsumeMessageAsync(cancellationToken);
                var schoolTask = SchoolConsumer.ConsumeMessageAsync(cancellationToken);
                var historyTask = HistoryConsumer.ConsumeMessageAsync(cancellationToken);
                await Task.WhenAll(userTask, testTask,questionTask,historyTask);

                // Optionally, add a delay or handle exceptions/logging here
            }
        }, _cancellationTokenSource.Token);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _cancellationTokenSource.Cancel();
        return Task.CompletedTask;
    }
}
