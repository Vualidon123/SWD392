using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SPTS_Reader.Entities;
using SPTS_Reader.Models;
using SPTS_Reader.Services;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace SPTS_Reader.EventBus
{
    public class QuestionConsumer
    {
        private readonly QuestionService _questionService;

        public QuestionConsumer(QuestionService questionService)
        {
            _questionService = questionService;
        }

        public async Task ConsumeMessageAsync(CancellationToken cancellationToken)
        {
            string _hostName = "localhost";
            string _queueName = "Question";

            var factory = new ConnectionFactory() { HostName = _hostName };

            using (var connection = await factory.CreateConnectionAsync())
            using (var channel = await connection.CreateChannelAsync())
            {
                await channel.QueueDeclareAsync(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                var consumer = new AsyncEventingBasicConsumer(channel);

                consumer.ReceivedAsync += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    string method = ea.BasicProperties.Headers != null && ea.BasicProperties.Headers.ContainsKey("method")
                        ? Encoding.UTF8.GetString((byte[])ea.BasicProperties.Headers["method"])
                        : string.Empty;
                    Question? question = JsonSerializer.Deserialize<Question>(message);

                    if (question != null)
                    {
                        try
                        {
                            switch (method)
                            {
                                case "create":
                                    // You may want to implement a method in TestService for User if needed
                                    // await _testService.AddUserAsync(userRequest);
                                    await _questionService.AddQuestionAsync(question);
                                    await channel.BasicAckAsync(ea.DeliveryTag, false);
                                    Console.WriteLine($"✅ Test added successfully: {message}");
                                    break;

                                case "update":
                                    // await _testService.UpdateUserAsync(userRequest);
                                    await _questionService.UpdateQuestionAsync(question);
                                    await channel.BasicAckAsync(ea.DeliveryTag, false);
                                    Console.WriteLine($"✅ Test updated successfully: {message}");
                                    break;

                                case "delete":
                                    if (question.Id != null)
                                    {
                                        // await _testService.DeleteUserAsync(userRequest.Id.ToString());
                                        await _questionService.DeleteQuestionAsync(question.Id.ToString());
                                        await channel.BasicAckAsync(ea.DeliveryTag, false);
                                        Console.WriteLine($"✅ Test deleted successfully: {message}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"❌ User deletion failed: Invalid ID");
                                        await channel.BasicNackAsync(ea.DeliveryTag, false, true);
                                    }
                                    break;

                                default:
                                    Console.WriteLine($"❌ Unknown method: {method}");
                                    await channel.BasicNackAsync(ea.DeliveryTag, false, true);
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"⚠️ Lỗi gửi Test : {ex.Message}. Thử lại...");
                            var properties = new BasicProperties();
                            properties.Persistent = true;
                            properties.Headers = new Dictionary<string, object> { { "method", method } };
                            await channel.BasicPublishAsync(exchange: "", routingKey: _queueName, mandatory: false, basicProperties: properties, body: body);
                            await channel.BasicAckAsync(ea.DeliveryTag, false);
                        }
                    }
                };

                await channel.BasicConsumeAsync(queue: _queueName, autoAck: false, consumer: consumer);

                // Keep the consumer alive until cancellation is requested
                var tcs = new TaskCompletionSource();
                using (cancellationToken.Register(() => tcs.SetResult()))
                {
                    await tcs.Task;
                }
            }
        }
    }
}
