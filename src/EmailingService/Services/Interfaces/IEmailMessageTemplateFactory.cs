using CSharpCourse.Core.Lib.Enums;
using EmailingService.Models;

namespace EmailingService.Services.Interfaces
{
    public interface IEmailMessageTemplateFactory
    {
        EmailMessageTemplate Create(EmployeeEventType employeeEventType, object? additionalData);
    }
}