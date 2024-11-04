using Microsoft.Extensions.DependencyInjection;

namespace Tienda.Utilities.Attributes;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services) =>
        services.RegisterInterfacesAndImplementations("Tienda.Contracts", "Tienda.Infrastructure");
}