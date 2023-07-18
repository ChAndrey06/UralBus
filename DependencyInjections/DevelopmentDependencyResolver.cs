using Common.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BLL = BLL.Logic;
using DE = DAL.Entities;
using DSQL = DAL.SQL;

namespace DependencyInjections;

//Override this class to register your services in development environment
public class DevelopmentDependencyResolver : ProductionDependencyResolver
{
    public DevelopmentDependencyResolver(IConfiguration configuration) : base(configuration)
    {
    }

    public override void RegisterDalServices(IServiceCollection services)
    {
        base.RegisterDalServices(services);
    }

    public override void RegisterBlServices(IServiceCollection services)
    {
        base.RegisterBlServices(services);
    }

    public override void RegisterPlServices(IServiceCollection services)
    {
        base.RegisterPlServices(services);
    }

    public override void RegisterServices(IServiceCollection services)
    {
        base.RegisterServices(services);
    }
    
    public override void RegisterCommonServices(IServiceCollection services)
    {
       // services.AddScoped<ISmsService, MockSmsService>();
        base.RegisterCommonServices(services);
    }
}