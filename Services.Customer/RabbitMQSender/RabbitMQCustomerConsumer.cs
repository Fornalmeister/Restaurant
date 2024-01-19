using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Services.Customer.Models.Dto;
using System.Text;

namespace Services.Customer.RabbitMQSender
{
    public class RabbitMQCustomerConsumer : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private IConnection _connection;
        private IModel _channel;
        public RabbitMQCustomerConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Password = "guest",
                UserName = "guest",
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(_configuration.GetValue<string>("TopicAndQueueNames:AddCustomerQueue")
                , false, false, false, null);

            var consumer = new EventingBasicConsumer(_channel);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                CustomersDto customerDto = JsonConvert.DeserializeObject<CustomersDto>(content);
               //HandleMessage(customerDto).GetAwaiter().GetResult();    

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(_configuration.GetValue<string>("TopicAndQueueNames:AddCustomerQueue"), false, consumer);

            return Task.CompletedTask;
        }

        private async Task HandleMessage(CustomersDto customerDto)
        {
            // here you cane handle with your message
        }

        }
}
