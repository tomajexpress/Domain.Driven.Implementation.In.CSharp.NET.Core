using EShoppingTutorial.Core.Application.Common;
using EShoppingTutorial.Core.Application.Orders;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EShoppingTutorial.Core.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddCoreApplicationConfigs(this IServiceCollection services)
    {
        var applicationAssembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg => {
             cfg.RegisterServicesFromAssembly(applicationAssembly);
             cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
         });

        services.AddValidatorsFromAssembly(applicationAssembly);

        services.AddAutoMapper(OrderMappingProfile.AddMappingConfigs);

        return services;
    }
}