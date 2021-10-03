using System.Threading;
using System.Threading.Tasks;
using CSharpCourse.Core.Lib.Events;
using EmailingService.Models;
using EmailingService.Services.Interfaces;

namespace EmailingService.Services
{
    public class NotificationEventHandlingService : INotificationEventHandlingService
    {
        private readonly IEmailMessageTemplateFactory _templateFactory;
        private readonly IEmailSender _emailSender;

        public NotificationEventHandlingService(IEmailMessageTemplateFactory templateFactory, IEmailSender emailSender)
        {
            _templateFactory = templateFactory;
            _emailSender = emailSender;
        }

        public async Task Handle(NotificationEvent notificationEvent, CancellationToken cancellationToken)
        {
            var template = _templateFactory.Create(notificationEvent.EventType, notificationEvent.Payload);
            var emailMessage = new EmailMessage(
                notificationEvent.EmployeeEmail,
                notificationEvent.EmployeeName,
                template.RenderSubject(notificationEvent.EmployeeName),
                template.RenderBody(notificationEvent.EmployeeName));
            await _emailSender.SendMessage(emailMessage, cancellationToken);
        }
    }
}