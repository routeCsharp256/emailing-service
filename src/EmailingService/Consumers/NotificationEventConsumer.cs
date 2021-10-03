using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpCourse.Core.Lib.Events;
using EmailingService.Configuration;
using EmailingService.Consumers.Interfaces;
using EmailingService.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EmailingService.Consumers
{
    public class NotificationEventConsumer : KafkaConsumerBase<NotificationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public NotificationEventConsumer(
            IOptions<KafkaTopics> kafkaTopics,
            IKafkaConsumerFactory kafkaConsumerFactory,
            ILogger<NotificationEventConsumer> logger,
            IServiceProvider serviceProvider)
            : base(kafkaTopics.Value.EmployeeNotificationEventTopic, kafkaConsumerFactory, logger)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task HandleMessage(NotificationEvent message, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var notificationEventHandlingService = scope.ServiceProvider.GetRequiredService<INotificationEventHandlingService>();
            await notificationEventHandlingService.Handle(message, cancellationToken);
        }
    }
}