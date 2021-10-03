using Confluent.Kafka;
using EmailingService.Configuration;
using EmailingService.Consumers.Interfaces;
using Microsoft.Extensions.Options;

namespace EmailingService.Consumers
{
    public class KafkaConsumerFactory : IKafkaConsumerFactory
    {
        private readonly KafkaOptions _kafkaOptions;

        public KafkaConsumerFactory(IOptions<KafkaOptions> kafkaOptions)
        {
            _kafkaOptions = kafkaOptions.Value;
        }
        
        public IConsumer<TKey, TValue> Create<TKey, TValue>()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _kafkaOptions.Servers,
                GroupId = _kafkaOptions.ConsumerGroup,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                HeartbeatIntervalMs = 1000
            };
            return new ConsumerBuilder<TKey, TValue>(config).Build();
        }
    }
}