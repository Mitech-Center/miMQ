using Asp.Versioning;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using myAPI.Contracts;
using myAPI.Contracts.MessageBroker.EventBus;
using myAPI.InfrastructureService.MessageBroker;
using myAPI.LoggerService;
using myAPI.Repository;
using myAPI.Service;
using myAPI.Service.Contracts;
using myAPI.Service.MessageService;

namespace myAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyOrigin());
        });
        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
        {

        });
        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();
        public static void ConfigureMessageBrokerService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MessageBrokerSettings>(
                configuration.GetSection("MessageBroker"));
            services.AddSingleton(sp=>sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);
            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();
                busConfigurator.AddConsumer<MessageConsumer>();
                busConfigurator.UsingRabbitMq((context, cfg) =>
                {
                    MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();
                    cfg.Host(new Uri(settings.Host), h=>
                    {
                        h.Username(settings.Username);
                        h.Password(settings.Password);
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });
            services.AddTransient<IEventBus, EventBus>();
        }
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
            });
        }
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Mi_API",
                    Version = "v1"
                });
                //s.SwaggerDoc("v2", new OpenApiInfo
                //{
                //    Title = "Code Maze API",
                //    Version = "v2"
                //});
                var xmlFile = $"{typeof(Presentation.AssemblyReference).Assembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);
            });
        }
    }

}
