namespace Services.Customer.RabbitMQSender
{
    public interface IRabbitMQCustomerMessageSender
    {
        void SendMessage(Object message, string queueName);
    }
}
