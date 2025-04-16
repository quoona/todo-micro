using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Rabbit.Application.Events.TodoEvents;
using Rabbit.Application.Interfaces;
using Rabbit.Application.Interfaces.Generic;
using Rabbit.Application.Interfaces.Logging;
using Rabbit.Application.Interfaces.Todos;
using Rabbit.Application.UseCases;
using Rabbit.Application.Validators;
using Rabbit.Infrastructure.Data;
using Rabbit.Infrastructure.Events.TodoEvents;
using Rabbit.Infrastructure.Repositories;
using Rabbit.Infrastructure.Repositories.Generic;
using Rabbit.Infrastructure.Services;

namespace Rabbit.API;

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

        //Add Controllers
        builder.Services.AddControllers();

        //Add Validators
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssemblyContaining<TodoDtoValidator>();

        //Repositories
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped<ITodoRepository, TodoRepository>();

        //Services
        builder.Services.AddScoped<ITodoService, TodoService>();
        builder.Services.AddScoped<ITodoService, TodoService>();

        //Mapper
        builder.Services.AddAutoMapper(cfg => { cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()); });

        //UseCases
        builder.Services.AddScoped<ICreateTodoUseCase, CreateTodoUseCase>();

        //Logging
        builder.Services.AddSingleton<ILoggingService, LoggingService>();

        //Mediator
        builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(TodoCreatedEventHandler).Assembly); });

        //MassTransit + RabbitMQ
        builder.Services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host("rabbitmq", "/", h =>
                {
                    h.Username("rabbitadmin");
                    h.Password("123456");
                });
            });
        });

        //DB Context
        var connectionString = builder.Configuration.GetConnectionString("Default");
        builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString,
                new MySqlServerVersion(ServerVersion.AutoDetect(connectionString))
            )
        );

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        //Map controller
        app.MapControllers();

        app.Run();
    }
}