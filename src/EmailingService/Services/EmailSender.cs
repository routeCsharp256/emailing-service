using System.Threading;
using System.Threading.Tasks;
using EmailingService.Configuration;
using EmailingService.Models;
using EmailingService.Services.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace EmailingService.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly SenderOptions _senderOptions;
        
        public EmailSender(IOptions<SenderOptions> senderOptions)
        {
            _senderOptions = senderOptions.Value;
        }

        public async Task SendMessage(EmailMessage emailMessage, CancellationToken cancellationToken)
        {
            var message = new MimeMessage
            {
                From = { new MailboxAddress(_senderOptions.SenderName, _senderOptions.SenderEmail) },
                To = { new MailboxAddress(emailMessage.RecipientName, emailMessage.Recipient) },
                Subject = emailMessage.Subject,
                Body = new TextPart(TextFormat.Html)
                {
                    Text = emailMessage.Body
                }
            };

            // TODO: Вынести это дело в отдельный пул с smtp клиентами.
            // singleton'ом использовать это нельзя, потому что не thread safe, но и коннектится каждый раз - не комильфо.
            using var client = new SmtpClient();
            await client.ConnectAsync(_senderOptions.SmtpHost, _senderOptions.SmtpPort, false, cancellationToken);
            await client.AuthenticateAsync(_senderOptions.SenderEmail, _senderOptions.SenderPassword, cancellationToken);
            await client.SendAsync(message, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);
        }
    }
}