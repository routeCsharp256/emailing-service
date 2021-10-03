using System.Threading;
using System.Threading.Tasks;
using EmailingService.Contracts.Requests;
using EmailingService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmailingService.Controllers
{
    [ApiController]
    [Route("email")]
    public class EmailController : ControllerBase
    {
        private readonly INotificationEventHandlingService _notificationEventHandlingService;

        public EmailController(INotificationEventHandlingService notificationEventHandlingService)
        {
            _notificationEventHandlingService = notificationEventHandlingService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send(SendEmailRequest request, CancellationToken cancellationToken)
        {
            await _notificationEventHandlingService.Handle(request.NotificationEvent, cancellationToken);
            return Ok();
        }
    }
}