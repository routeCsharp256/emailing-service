using System.Threading;
using System.Threading.Tasks;
using EmailingService.Models;

namespace EmailingService.Services.Interfaces
{
    public interface IEmailSender
    {
        Task SendMessage(EmailMessage emailMessage, CancellationToken cancellationToken);
    }
}