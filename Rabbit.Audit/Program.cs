using MassTransit;
using Rabbit.Audit.Application.Interfaces;
using Rabbit.Audit.Consumers;
using Rabbit.Audit.Infrastructure.Services;

namespace Rabbit.Audit;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Logging.SetMinimumLevel(LogLevel.Debug);

        builder.Services.AddScoped<AuditLogConsumer>();
        
        builder.Services.AddScoped<ITelegramMessageService, TelegramMessageService>();

        // Logging
        builder.Services.AddSingleton<ILoggingService, LoggingService>();

        //MassTransit + RabbitMQ
        builder.Services.AddMassTransit(x =>
        {
            x.AddConsumer<AuditLogConsumer>();
            x.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host("rabbitmq", "/", h =>
                {
                    h.Username("rabbitadmin");
                    h.Password("123456");
                    h.Heartbeat(TimeSpan.FromSeconds(10));
                });

                configurator.ReceiveEndpoint("audit-log-queue",
                    e => { e.ConfigureConsumer<AuditLogConsumer>(context); });
                
                configurator.ConfigureEndpoints(context);
                
                configurator.UseDelayedRedelivery(r => r.Interval(3, TimeSpan.FromSeconds(5)));
            });
        });


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.Run();
    }
}