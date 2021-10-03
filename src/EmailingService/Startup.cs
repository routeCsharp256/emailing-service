using EmailingService.Configuration;
using EmailingService.Consumers;
using EmailingService.Consumers.Interfaces;
using EmailingService.Services;
using EmailingService.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace EmailingService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<SenderOptions>(Configuration.GetSection(nameof(SenderOptions)));
            services.Configure<KafkaOptions>(Configuration.GetSection(nameof(KafkaOptions)));
            services.Configure<KafkaTopics>(Configuration.GetSection(nameof(KafkaTopics)));
            
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IEmailMessageTemplateFactory, EmailMessageTemplateFactory>();
            services.AddScoped<INotificationEventHandlingService, NotificationEventHandlingService>();
            services.AddSingleton<IKafkaConsumerFactory, KafkaConsumerFactory>();

            services.AddHostedService<NotificationEventConsumer>();

            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EmailingService", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmailingService v1"));
            
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}