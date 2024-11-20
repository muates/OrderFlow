namespace SharedLibrary.Producer.Abstract;

public interface IMessageSender<in TMessage>
{
    Task SendMessageAsync(TMessage message, string routingKey, string exchange, string exchangeType = "direct");
}