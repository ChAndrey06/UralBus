using Common.Helpers;
using Microsoft.Extensions.Configuration;
using Environment = Common.Enums.Environment;

namespace DependencyInjections;

public static class ResolverFactory
{
    public static IDependencyResolver GetDependencyResolver(this IConfiguration configuration)
    {
        var environment = EnvironmentHelper.GetEnvironmentType(configuration["Environment"]);
        
        return environment switch
        {
            Environment.Production => new ProductionDependencyResolver(configuration),
            Environment.Development => new DevelopmentDependencyResolver(configuration),
            _ => throw new InvalidOperationException($"The environment '{environment}' is not supported.")
        };
    }
}