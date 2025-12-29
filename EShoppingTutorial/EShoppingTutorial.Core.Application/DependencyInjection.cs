using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EShoppingTutorial.Core.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreApplication(this IServiceCollection services)
    {
        return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}