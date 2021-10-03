namespace EmailingService.Models
{
    public class EmailMessageTemplate
    {
        public const string NamePlaceholder = "$repicient_name$";

        public EmailMessageTemplate(string templateSubject, string templateBody)
        {
            TemplateSubject = templateSubject;
            TemplateBody = templateBody;
        }

        public string TemplateSubject { get; }
        
        public string TemplateBody { get; }

        // TODO: Выделить отдельный renderer.
        public string RenderSubject(string recipientName)
        {
            return TemplateSubject.Replace(NamePlaceholder, recipientName);
        }

        public string RenderBody(string recipientName)
        {
            return TemplateBody.Replace(NamePlaceholder, recipientName);
        }
    }
}