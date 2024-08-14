using KafkaFlow;
using KafkaFlow.Serializer;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Kafka;

public static class Kafka
{
    public static void AddKafka(this IServiceCollection services)
    {
        const string producerName = "PrincipalProducer";
        const string topicName = "book-topic";

        services.AddKafka(
            kafka => kafka
                .UseConsoleLog()
                .AddCluster(
                    cluster => cluster
                        .WithBrokers(new[] { "localhost:9092" })
                        .CreateTopicIfNotExists(topicName, 3, 1)
                        .AddProducer(
                            producerName,
                            producer => producer
                                .DefaultTopic(topicName)
                                .AddMiddlewares(m => m.AddSerializer<ProtobufNetSerializer>())
                        )
                        .AddConsumer(
                            consumer => consumer
                                .Topic(topicName)
                                .WithGroupId("book-handler")
                                .WithBufferSize(50)
                                .WithWorkersCount(3)
                                .AddMiddlewares(
                                    middlewares => middlewares
                                        .AddDeserializer<ProtobufNetDeserializer>()
                                        .AddTypedHandlers(h => h.AddHandler<MessageHandler>())
                                )
                        )
                )
        );
    }
}
