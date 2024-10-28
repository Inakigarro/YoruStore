using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Tienda.Utilities.Attributes;

public static class AttributeRegistration
{
    public static IServiceCollection RegisterInterfacesAndImplementations(this IServiceCollection serviceCollection,
        string interfaceName, string implementationName)
    {
        var interfaceAssembly = Assembly.Load(interfaceName);
        var implementationAssembly = Assembly.Load(implementationName);

        // Todas las clases no abstractas.
        var types = implementationAssembly.GetExportedTypes().Where(type => type.IsClass && !type.IsAbstract);
        foreach (var type in types)
        {
            var interfaceType = type.GetInterfaces().FirstOrDefault();
            if (interfaceType is not null && interfaceType.Assembly == interfaceAssembly)
            {
                var attributeNames = type.GetCustomAttributes(true).Select(attr => attr.GetType().Name).ToList();

                switch (true)
                {
                    case var _ when attributeNames.Contains(nameof(ScopedAttribute)):
                        serviceCollection.AddScoped(interfaceType, type);
                        break;
                    case var _ when attributeNames.Contains(nameof(TransientAttribute)):
                        serviceCollection.AddTransient(interfaceType, type);
                        break;
                    default:
                        serviceCollection.AddTransient(interfaceType, type);
                        break;
                }
            }
        }

        return serviceCollection;
    }
}