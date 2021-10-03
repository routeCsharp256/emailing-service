using System.Threading;
using System.Threading.Tasks;
using CSharpCourse.Core.Lib.Events;

namespace EmailingService.Services.Interfaces
{
    public interface INotificationEventHandlingService
    {
        Task Handle(NotificationEvent notificationEvent, CancellationToken token);
    }
}