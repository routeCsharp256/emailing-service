using CSharpCourse.Core.Lib.Events;

namespace EmailingService.Contracts.Requests
{
    public class SendEmailRequest
    {
        public NotificationEvent NotificationEvent { get; set; }
    }
}