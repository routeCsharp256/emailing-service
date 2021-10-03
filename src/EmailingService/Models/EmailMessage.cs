namespace EmailingService.Models
{
    public class EmailMessage
    {
        public EmailMessage(string recipient, string recipientName, string subject, string body)
        {
            Recipient = recipient;
            RecipientName = recipientName;
            Subject = subject;
            Body = body;
        }

        public string Recipient { get; }
        
        public string RecipientName { get; }
        
        public string Subject { get; }
        
        public string Body { get; }
    }
}