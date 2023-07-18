using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjections;

public interface IDependencyResolver
{
    void RegisterDalServices(IServiceCollection services);
    void RegisterBlServices(IServiceCollection services);
    void RegisterPlServices(IServiceCollection services);
    void RegisterServices(IServiceCollection services);
}