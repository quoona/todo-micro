using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Rabbit.Application.Interfaces;
using Rabbit.Application.Validators;
using Rabbit.Infrastructure.Data;
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

        //Services
        builder.Services.AddScoped<ITodoService, TodoService>();
        builder.Services.AddAutoMapper(cfg => { cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()); });

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