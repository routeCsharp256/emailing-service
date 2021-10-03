using Confluent.Kafka;

namespace EmailingService.Consumers.Interfaces
{
    public interface IKafkaConsumerFactory
    {
        IConsumer<TKey, TValue> Create<TKey, TValue>();
    }
}