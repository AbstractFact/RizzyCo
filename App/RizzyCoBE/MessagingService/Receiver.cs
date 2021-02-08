using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RizzyCoBE.MessagingService.Options;
using Microsoft.Extensions.Options;
using DTOs;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;

namespace RizzyCoBE.MessagingService
{
    public class Receiver : BackgroundService
    {
        private readonly MessageHub _hub;
        private readonly string _hostname;
        private readonly string _password;
        private readonly string _username;
        private IConnection _connection;
        private IModel _channel;

        public Receiver(MessageHub hub, IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _hub = hub;
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
                _channel = _connection.CreateModel();
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

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (ch, ea) =>
            {
                var body = Encoding.UTF8.GetString(ea.Body.ToArray());
                var message = JsonConvert.DeserializeObject<TestDTO>(body);
                await _hub.SendTestMessage(message);

                _channel.BasicAck(ea.DeliveryTag, false);

            };

            _channel.BasicConsume(queue: "counter", autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }

        public void ReceiveMessageFromQ()
        {
            if (ConnectionExists())
            {
                try
                {
                    _channel.QueueDeclare(queue: "counter",
                                            durable: true,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);

                    _channel.BasicQos(prefetchSize: 0, prefetchCount: 3, global: false);

                    var consumer = new EventingBasicConsumer(_channel);
                    consumer.Received += async (model, ea) =>
                    {
                        var body = Encoding.UTF8.GetString(ea.Body.ToArray());
                        var message = JsonConvert.DeserializeObject<TestDTO>(body);
                        await _hub.SendTestMessage(message);

                        _channel.BasicAck(ea.DeliveryTag, false);
                    };

                    _channel.BasicConsume(queue: "counter",
                                            autoAck: false,
                                            consumer: consumer);
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message} | {ex.StackTrace}");
                }
            }
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
