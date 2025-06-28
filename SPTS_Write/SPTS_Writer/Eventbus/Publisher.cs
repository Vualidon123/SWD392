using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using SPTS_Writer.Entities;
using System.Text;
using System.Text.Json;

namespace SPTS_Writer.Eventbus
{
    public class Publisher
    {   
        public Publisher()
        {
        }

        public async Task SendMessageAsync(Test test)
        {
            string _hostName = "localhost"; // or the RabbitMQ server address
            string _queueName = "Test";
            try
            {
                var factory = new ConnectionFactory() { HostName = _hostName };

                using (var connection = await factory.CreateConnectionAsync())
                using (var channel = await connection.CreateChannelAsync())
                {
                    if (connection.IsOpen && channel.IsOpen)
                    {

                        // Declare the queue
                        await channel.QueueDeclareAsync(queue: _queueName,
                                                        durable: false,
                                                        exclusive: false,
                                                        autoDelete: false,
                                                        arguments: null);

                        // Serialize the request object to JSON
                        var message = JsonSerializer.Serialize(test);
                        var body = Encoding.UTF8.GetBytes(message);

                        // Create basic properties and add retryCount header
                        var properties = new BasicProperties
                        {
                            Headers = new Dictionary<string, object> { { "retryCount", 0 } }
                        };

                        // Publish the message
                        await channel.BasicPublishAsync(exchange: "",
                                                        routingKey: _queueName,
                                                        mandatory: false,
                                                        basicProperties: properties,
                                                        body: body);


                    }
                    else
                    {
                        Console.WriteLine("Connection or channel is not open.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
                throw; // Re-throw the exception for handling by the caller
            }
        }
    }
}
