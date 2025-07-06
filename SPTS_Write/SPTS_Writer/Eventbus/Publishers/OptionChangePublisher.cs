using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using SPTS_Writer.Entities;
using System.Text;
using System.Text.Json;

namespace SPTS_Writer.Eventbus.Publishers
{
    public class OptionChangePublish
    {
        public OptionChangePublish()
        {
        }

        public async Task SendMessageAsync(Option option
            , string method)
        {
            string _hostName = "localhost"; // or the RabbitMQ server address
            string _queueName = "Option";
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
                        var message = JsonSerializer.Serialize(option);
                        var body = Encoding.UTF8.GetBytes(message);

                        // Create basic properties and add retryCount header
                        var properties = new BasicProperties
                        {
                            Headers = new Dictionary<string, object> { { "method", method } }
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
