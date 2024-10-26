using Aqsa.Domain.Common.Identity.UserRegistration;
using Aqsa.Domain.Common.Interceptors;
using Aqsa.Domain.Common.Services.Cache;
using Aqsa.Domain.Common.Services.EmailSender;
using FluentValidation;
using MediatR;
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
        services.AddValidatorsFromAssemblyContaining<CreateRegistrationRequest>();
        
        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssemblyContaining<RedisCacheService>();
            conf.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }); 
        
        // services.AddScoped<IValidator<CreateRegistrationRequestCommand>, CreateRegistrationRequestCommandValidator>();
        // services.AddMediatR(cfg => {
        //     cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        //     cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        //     cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
        //     cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        //     cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        // });
        return services;
    }
}
