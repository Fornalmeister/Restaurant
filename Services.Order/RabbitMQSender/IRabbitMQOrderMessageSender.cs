namespace Services.Order.RabbitMQSender
{
    public interface IRabbitMQOrderMessageSender
    {
        void SendMessage(Object message, string exchangeName);
    }
}
