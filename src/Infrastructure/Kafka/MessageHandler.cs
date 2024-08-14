using KafkaFlow;

namespace Infrastructure.Kafka;

public class MessageHandler : IMessageHandler<Message>
{
    public Task Handle(IMessageContext context, Message message)
    {
        Console.WriteLine(
            "Partition: {0} | Offset: {1} | Message: {2}",
            context.ConsumerContext.Partition,
            context.ConsumerContext.Offset,
            message.Text);

        return Task.CompletedTask;
    }
}

public class Message
{
    public string Text { get; set; } = default!;
};
