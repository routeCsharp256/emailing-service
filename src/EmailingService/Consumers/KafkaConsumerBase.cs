using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using EmailingService.Consumers.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EmailingService.Consumers
{
    public abstract class KafkaConsumerBase<TMessage> : BackgroundService
    {
        protected string TopicName { get; }
        
        private readonly IKafkaConsumerFactory _kafkaConsumerFactory;
        private readonly ILogger<KafkaConsumerBase<TMessage>> _logger;

        public KafkaConsumerBase(
            string? topicName,
            IKafkaConsumerFactory kafkaConsumerFactory,
            ILogger<KafkaConsumerBase<TMessage>> logger)
        {
            TopicName = topicName ?? throw new ArgumentNullException(nameof(topicName));
            _kafkaConsumerFactory = kafkaConsumerFactory;
            _logger = logger;
        }
        
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using var consumer = _kafkaConsumerFactory.Create<Ignore, string>();
            consumer.Subscribe(TopicName);
            while (!cancellationToken.IsCancellationRequested)
            {
                var consumeResult = consumer.Consume(cancellationToken);
                var deserializedMessage = DeserializeConsumeResult(consumeResult.Message.Value);
                await HandleMessage(deserializedMessage, cancellationToken);
                TryCommit(consumer, consumeResult);
            }

            consumer.Close();
        }

        protected virtual TMessage DeserializeConsumeResult(string value)
        {
            return JsonConvert.DeserializeObject<TMessage>(value);
        }

        protected abstract Task HandleMessage(TMessage message, CancellationToken cancellationToken);

        protected void TryCommit(IConsumer<Ignore, string> consumer, ConsumeResult<Ignore, string> consumeResult)
        {
            try
            {
                consumer.Commit(consumeResult);
            }
            catch (KafkaException exception)
            {
                _logger.LogError($"Could not commit message topic {consumeResult.Topic} offset {consumeResult.Offset}", exception);
            }
        }
    }
}