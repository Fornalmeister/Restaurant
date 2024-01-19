using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Services.OrderItem.Data;
using Services.OrderItem.Models;
using Services.OrderItem.Models.Dto;
using System.Text;

namespace Services.Customer.RabbitMQSender
{
    public class RabbitMQOrderConsumer : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private IConnection _connection;
        private IModel _channel;
        string queueName = "";
        public RabbitMQOrderConsumer(IConfiguration configuration)
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
            _channel.ExchangeDeclare(_configuration.GetValue<string>("TopicAndQueueNames:AddOrderQueue"), ExchangeType.Fanout);
            queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queueName, _configuration.GetValue<string>("TopicAndQueueNames:AddOrderQueue"), "");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                OrdersDto orderDto = JsonConvert.DeserializeObject<OrdersDto>(content);
                //HandleMessage(orderDto).GetAwaiter().GetResult();    

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(queueName, false, consumer);

            return Task.CompletedTask;
        }

        private async Task HandleMessage(OrdersDto orderDto)
        {
            // you can handle your massage here
        }

        }
}
