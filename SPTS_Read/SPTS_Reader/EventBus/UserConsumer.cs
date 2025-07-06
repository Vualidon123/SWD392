using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SPTS_Reader.Entities;
using SPTS_Reader.Models;
using SPTS_Reader.Services;
using SPTS_Reader.Services.Abstraction;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace SPTS_Reader.EventBus
{
    public class UserConsumer
    {
        private readonly UserService _userService;

        public UserConsumer(UserService userService)
        {
            _userService = userService;
        }

        public async Task ConsumeMessageAsync(CancellationToken cancellationToken)
        {
            string _hostName = "localhost";
            string _queueName = "User";

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
                    User? userRequest = JsonSerializer.Deserialize<User>(message);

                    if (userRequest != null)
                    {
                        try
                        {
                            switch (method)
                            {
                                case "create":
                                    await _userService.AddUserAsync(userRequest);
                                    await channel.BasicAckAsync(ea.DeliveryTag, false);
                                    Console.WriteLine($"✅ User added successfully: {message}");
                                    break;

                                case "update":
                                    await _userService.UpdateUserAsync(userRequest);
                                    await channel.BasicAckAsync(ea.DeliveryTag, false);
                                    Console.WriteLine($"✅ User updated successfully: {message}");
                                    break;

                                case "delete":
                                    if (userRequest.Id != null)
                                    {
                                        await _userService.DeleteUserAsync(userRequest.Id.ToString());
                                        await channel.BasicAckAsync(ea.DeliveryTag, false);
                                        Console.WriteLine($"✅ User deleted successfully: {message}");
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
