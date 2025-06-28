

using SPTS_Reader.EventBus;
using System.Threading;

namespace SPTS_Reader.Service
{
    public class ConsumerHostService : IHostedService
    {   
        private readonly Consumer _consumer;
        private CancellationTokenSource _cancellationTokenSource;
        public ConsumerHostService(Consumer consumer)
        {
            _consumer = consumer ?? throw new ArgumentNullException(nameof(consumer));  
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            Task.Run(_consumer.ConsumeMessageAsync, _cancellationTokenSource.Token);
            return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cancellationTokenSource.Cancel();
            return Task.CompletedTask;
        }
    }
}
