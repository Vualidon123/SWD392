using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SPTS_Reader.Entities;
using SPTS_Reader.Models;

using SPTS_Reader.Services;
using System.Text;
using System.Text.Json;

namespace SPTS_Reader.EventBus
{
    public class TestConsumer 
    {
        private  readonly TestService _testService;
       
        public TestConsumer(TestService testService)
        {
            _testService = testService;
        }
        public async Task<bool> ConsumeMessageAsync()
        {
            string _hostName = "localhost";
            string _queueName = "Test";

            var factory = new ConnectionFactory() { HostName = _hostName };
            var check = true;

            using (var connection = await factory.CreateConnectionAsync())
            using (var channel = await connection.CreateChannelAsync())
            {
                await channel.QueueDeclareAsync(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                var consumer = new AsyncEventingBasicConsumer(channel);

                // Use TaskCompletionSource to keep the method alive until a message is processed
                var tcs = new TaskCompletionSource<bool>();

                consumer.ReceivedAsync += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    int retryCount = ea.BasicProperties.Headers != null && ea.BasicProperties.Headers.ContainsKey("retryCount") ? (int)ea.BasicProperties.Headers["retryCount"] : 0;
                    Test? testRequest = null;
                    testRequest = JsonSerializer.Deserialize<Test>(message);

                    if (testRequest != null)
                    {
                        try
                        {
                            await _testService.AddTestAsync(testRequest);
                            await channel.BasicAckAsync(ea.DeliveryTag, false);
                            Console.WriteLine($"✅ Test gửi thành công: {message}");
                            tcs.TrySetResult(true); // Signal completion
                        }
                        catch (Exception ex)
                        {
                            retryCount++;
                            if (retryCount > 3)
                            {
                                Console.WriteLine($"❌ Đưa vào DLQ: {message}");
                                check = false;
                                await channel.BasicNackAsync(ea.DeliveryTag, false, false);
                                tcs.TrySetResult(false); // Signal completion
                            }
                            else
                            {
                                Console.WriteLine($"⚠️ Lỗi gửi Test ({retryCount}/{3}): {ex.Message}. Thử lại...");
                                var properties = new BasicProperties();
                                properties.Persistent = true;
                                properties.Headers = new Dictionary<string, object> { { "retryCount", retryCount } };
                                await channel.BasicPublishAsync(exchange: "", routingKey: _queueName, mandatory: false, basicProperties: properties, body: body);
                                await channel.BasicAckAsync(ea.DeliveryTag, false);
                                tcs.TrySetResult(false); // Signal completion
                            }
                        }
                    }
                };

                await channel.BasicConsumeAsync(queue: _queueName, autoAck: false, consumer: consumer);

                // Wait until a message is processed
                await tcs.Task;
            }
            return check;
        }
    }
}
