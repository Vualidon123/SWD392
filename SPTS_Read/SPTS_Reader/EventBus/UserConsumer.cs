using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SPTS_Reader.Entities;
using SPTS_Reader.Models;
using SPTS_Reader.Services;
using SPTS_Reader.Services.Abstraction;
using System.Text;
using System.Text.Json;

namespace SPTS_Reader.EventBus
{
    public class UserConsumer
    {
        private readonly UserService _userService;

        public UserConsumer(UserService userService)
        {
            _userService = userService;
        }
        public async Task<bool> ConsumeMessageAsync()
        {
            string _hostName = "localhost";
            string _queueName = "User";

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
                    string method = ea.BasicProperties.Headers != null && ea.BasicProperties.Headers.ContainsKey("method")
                       ? Encoding.UTF8.GetString((byte[])ea.BasicProperties.Headers["method"])
                       : string.Empty;
                    User? userRequest = null;
                    userRequest = JsonSerializer.Deserialize<User>(message);

                    if (userRequest != null)
                    {
                        try
                        {
                            /*await _userService.AddUserAsync(userRequest);
                            await channel.BasicAckAsync(ea.DeliveryTag, false);
                            Console.WriteLine($"✅ Test gửi thành công: {message}");
                            tcs.TrySetResult(true); // Signal completion*/
                            switch (method)
                            {
                                case "create":
                                    await _userService.AddUserAsync(userRequest);
                                    await channel.BasicAckAsync(ea.DeliveryTag, false);
                                    Console.WriteLine($"✅ User added successfully: {message}");
                                    tcs.TrySetResult(true); // Signal completion
                                    break;

                                case "update":
                                    await _userService.UpdateUserAsync(userRequest);
                                    await channel.BasicAckAsync(ea.DeliveryTag, false);
                                    Console.WriteLine($"✅ User updated successfully: {message}");
                                    tcs.TrySetResult(true); // Signal completion
                                    break;

                                case "delete":
                                    if (userRequest.Id != null)
                                    {
                                        await _userService.DeleteUserAsync(userRequest.Id.ToString());
                                        await channel.BasicAckAsync(ea.DeliveryTag, false);
                                        Console.WriteLine($"✅ User deleted successfully: {message}");
                                        tcs.TrySetResult(true); // Signal completion
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
/*                            retryCount++;
                            if (retryCount > 3)
                            {
                                Console.WriteLine($"❌ Đưa vào DLQ: {message}");
                                check = false;
                                await channel.BasicNackAsync(ea.DeliveryTag, false, false);
                                tcs.TrySetResult(false); // Signal completion
                            }*/
                            
                            
                                Console.WriteLine($"⚠️ Lỗi gửi Test : {ex.Message}. Thử lại...");
                                var properties = new BasicProperties();
                                properties.Persistent = true;
                                properties.Headers = new Dictionary<string, object> { { "method", method } };
                                await channel.BasicPublishAsync(exchange: "", routingKey: _queueName, mandatory: false, basicProperties: properties, body: body);
                                await channel.BasicAckAsync(ea.DeliveryTag, false);
                                tcs.TrySetResult(false); // Signal completion
                            
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
