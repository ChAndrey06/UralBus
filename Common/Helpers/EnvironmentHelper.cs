using Microsoft.Extensions.Configuration;
using Environment = Common.Enums.Environment;

namespace Common.Helpers;

public static class EnvironmentHelper
{
    public static Environment GetEnvironmentType(this IConfiguration configuration)
    {
        var env = configuration["Environment"];
        return GetEnvironmentType(env);
    }
    
    public static Environment GetEnvironmentType(string? env)
    {
        if (string.IsNullOrEmpty(env))
        {
            return Environment.Development;
        }

        if (Enum.TryParse(env, true, out Environment environmentType))
        {
            return environmentType;
        }

        return Environment.Development;
    }
}