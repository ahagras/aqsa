using System.Reflection;
using Aqsa.API.Middleware;
using Aqsa.Domain;
using Aqsa.Domain.Common;
using Aqsa.Domain.Common.Identity.UserRegistration;
using Aqsa.Domain.Common.Models;
using Aqsa.Domain.Common.Services.Cache;
using Aqsa.Domain.Common.Services.EmailSender;
using Aqsa.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddValidatorsFromAssemblyContaining<CreateRegistrationRequestCommandValidator>();

builder.Services.AddDbContext<IJADbContext,JADbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

builder.Services.AddValidatorsFromAssemblyContaining<CreateRegistrationRequest>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddMediatR(conf =>
    conf.RegisterServicesFromAssemblyContaining<Program>());

        builder.Services.AddDomainConfigurations("127.0.0.1:6379");

        // builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("127.0.0.1:6379"));
        //
        // builder.Services.AddScoped<IRedisCacheService, RedisCacheService>();
        // builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(options => { });

app.UseHttpsRedirection();

app.MapPost("/RegistrationRequest", async (CreateRegistrationRequest request, IMediator mediator) => await mediator.Send(request))
    .WithName("RegistrationRequest")
    .WithOpenApi();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}