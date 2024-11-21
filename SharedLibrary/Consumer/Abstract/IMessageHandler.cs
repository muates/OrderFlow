namespace SharedLibrary.Consumer.Abstract;

public interface IMessageHandler<in TMessage>
{
    void HandleMessage(TMessage message);   
}