using Aqsa.Domain.Common.Services.Cache;
using Aqsa.Domain.Common.Services.EmailSender;
using Aqsa.Domain.Identity.UserRegistration.CreateRegistrationRequest;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Aqsa.Domain.Common;

public static class ConfigureServices
{
    public static IServiceCollection AddDomainConfigurations(
    this IServiceCollection services, string cacheConnectionString)
    {
        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(cacheConnectionString));

        services.AddScoped<IRedisCacheService, RedisCacheService>();
        services.AddScoped<IEmailSenderService, EmailSenderService>();

        services.AddMediatR(conf => conf.RegisterServicesFromAssemblyContaining<RedisCacheService>()); 
        services.AddValidatorsFromAssemblyContaining<CreateRegistrationRequestCommandValidator>();
        // services.AddScoped<IValidator<CreateRegistrationRequestCommand>, CreateRegistrationRequestCommandValidator>();
        
        return services;
    }
}
