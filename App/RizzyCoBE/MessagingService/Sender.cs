using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RizzyCoBE.MessagingService.Options;
using Microsoft.Extensions.Options;
using DTOs;

namespace RizzyCoBE.MessagingService
{
    public class Sender
    {
        private readonly string _hostname;
        private readonly string _password;
        private readonly string _username;
        private IConnection _connection;

        public Sender(IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _hostname = rabbitMqOptions.Value.Hostname;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;

            CreateConnection();
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostname,
                    UserName = _username,
                    Password = _password
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }

            CreateConnection();

            return _connection != null;
        }
        public void PushMessageToQ(TestDTO msg)
        {
            if (ConnectionExists())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "counter",
                                    durable: true,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                    channel.ExchangeDeclare(exchange: "invitations", type: ExchangeType.Fanout);

                    channel.QueueBind(queue: "counter",
                                       exchange: "invitations",
                                       routingKey: "counter");

                    var json = JsonConvert.SerializeObject(msg);
                    var messageBody = Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish(exchange: "invitations", routingKey: "counter", body: messageBody, basicProperties: null);
                }  
            }
          
        }
    }
}
