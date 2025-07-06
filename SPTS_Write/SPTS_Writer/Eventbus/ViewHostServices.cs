
using SPTS_Writer.Eventbus.ViewChanges;

namespace SPTS_Writer.Eventbus
{
    public class ViewHostServices : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private CancellationTokenSource _cancellationTokenSource;

        public ViewHostServices(IServiceScopeFactory scopeFactory)
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
                    var userView = scope.ServiceProvider.GetRequiredService<UserView>();
                    var testView = scope.ServiceProvider.GetRequiredService<TestView>();
                    var questionView = scope.ServiceProvider.GetRequiredService<QuestionView>();
                    var SchoolView = scope.ServiceProvider.GetRequiredService<SchoolView>();
                    var historyView = scope.ServiceProvider.GetRequiredService<HistoryView>();
                    // Run both consumers in parallel and wait for both to finish
                    var userTask = userView.SyncUserSnapshotWithUsersAsync(cancellationToken);
                    var testTask = testView.SyncTestSnapshotWithTestsAsync(cancellationToken);
                    var questionTask = questionView.SyncTestSnapshotWithTestsAsync(cancellationToken);
                    var schoolTask = SchoolView.SyncTestSnapshotWithTestsAsync(cancellationToken);
                    var historyTask = historyView.SyncTestSnapshotWithTestsAsync(cancellationToken);
                    await Task.WhenAll(userTask, testTask,questionTask,schoolTask,historyTask);

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
}
