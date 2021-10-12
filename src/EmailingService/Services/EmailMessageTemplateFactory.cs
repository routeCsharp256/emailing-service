using System;
using CSharpCourse.Core.Lib.Enums;
using CSharpCourse.Core.Lib.Events;
using EmailingService.Extensions;
using EmailingService.Models;
using EmailingService.Services.Interfaces;

namespace EmailingService.Services
{
    public class EmailMessageTemplateFactory : IEmailMessageTemplateFactory
    {
        public EmailMessageTemplate Create(EmployeeEventType employeeEventType, object? additionalData)
        {
            return employeeEventType switch
            {
                EmployeeEventType.Hiring => TemplatePool.Hiring,
                EmployeeEventType.ProbationPeriodEnding => TemplatePool.ProbationPeriodEnding,
                EmployeeEventType.Dismissal => TemplatePool.Dismissal,
                EmployeeEventType.ConferenceAttendance => TemplatePool.ConferenceAttendance,
                EmployeeEventType.MerchDelivery => CreateMerchDeliveryPayload(additionalData),
                _ => throw new ArgumentOutOfRangeException(nameof(employeeEventType))
            };
        }

        private static EmailMessageTemplate CreateMerchDeliveryPayload(object? additionalData)
        {
            var payload = EnsurePayloadIsNotNull(additionalData);
            return GetTemplateByMerchType(payload.MerchType);
        }

        private static MerchDeliveryEventPayload EnsurePayloadIsNotNull(object? additionalData)
        {
            if (additionalData is null)
            {
                throw new ArgumentNullException(nameof(additionalData));
            }

            return PayloadDeserializer.Get<MerchDeliveryEventPayload>(additionalData);
        }

        private static EmailMessageTemplate GetTemplateByMerchType(MerchType merchType)
        {
            return merchType switch
            {
                MerchType.WelcomePack => TemplatePool.MerchDelivery.WelcomePack,
                MerchType.ProbationPeriodEndingPack => TemplatePool.MerchDelivery.ProbationPeriodEndingPack,
                MerchType.ConferenceListenerPack => TemplatePool.MerchDelivery.ConferenceListenerPack,
                MerchType.ConferenceSpeakerPack => TemplatePool.MerchDelivery.ConferenceSpeakerPack,
                MerchType.VeteranPack => TemplatePool.MerchDelivery.VeteranPack,
                _ => throw new ArgumentOutOfRangeException(nameof(merchType))
            };
        }
    }
}