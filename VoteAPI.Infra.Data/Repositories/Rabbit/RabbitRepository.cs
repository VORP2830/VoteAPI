using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using VoteAPI.Domain.Entities.Rabbit;
using VoteAPI.Domain.Interfaces.Rabbit;
using Microsoft.Extensions.Configuration;

namespace VoteAPI.Infra.Data.Repositories.Rabbit
{
    public class RabbitRepository : IRabbitRepository
    {
        private readonly string RabbitMQHost;
        private readonly string RabbitMQUser;
        private readonly string RabbitMQPassword;
        private readonly string RabbitMQPort;
        public RabbitRepository(IConfiguration configuration)
        {
            RabbitMQHost = Environment.GetEnvironmentVariable("RABITMQHOST") ?? configuration.GetSection("RabbitMQ").GetSection("Host").Value;
            RabbitMQUser = Environment.GetEnvironmentVariable("RABITMQUSER") ?? configuration.GetSection("RabbitMQ").GetSection("User").Value;
            RabbitMQPassword = Environment.GetEnvironmentVariable("RABITMQPASSWORD") ?? configuration.GetSection("RabbitMQ").GetSection("Password").Value;
            RabbitMQPort = Environment.GetEnvironmentVariable("RABITMQPORT") ?? configuration.GetSection("RabbitMQ").GetSection("Port").Value;
        }
        public void SendMessage(RabbitMessage message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = RabbitMQHost,
                Port = int.Parse(RabbitMQPort),
                UserName = RabbitMQUser,
                Password = RabbitMQPassword
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "VotationResult",
                                    durable: true,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                var json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "",
                                    routingKey: "VotationResult",
                                    basicProperties: null,
                                    body: body);
            }
        }
    }
}
