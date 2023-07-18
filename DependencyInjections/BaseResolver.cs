using AutoMapper;
using BLL.Interfaces;
using BLL.Logic;
using Common.Helpers;
using Common.Interfaces;
using Common.Mapping;
using DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Environment = Common.Enums.Environment;

namespace DependencyInjections;

public abstract class BaseResolver : IDependencyResolver
{
    protected readonly bool IsDevelopment;
    protected readonly Environment Environment;

    protected BaseResolver(IConfiguration configuration)
    {
        Environment = configuration.GetEnvironmentType();
        IsDevelopment = Environment == Environment.Development;
    }


    public abstract void RegisterDalServices(IServiceCollection services);
    public abstract void RegisterBlServices(IServiceCollection services);
    public abstract void RegisterPlServices(IServiceCollection services);
    
    public abstract void RegisterCommonServices(IServiceCollection services);
    
    public virtual void RegisterServices(IServiceCollection services)
    {
        RegisterDalServices(services);
        RegisterBlServices(services);
        RegisterPlServices(services);
        RegisterCommonServices(services);
    }
    

    protected void AddCrudServices<TDalEntity, TBllEntity, TKey>(IServiceCollection services)
        where TDalEntity : class, IEntity<TKey>
		where TBllEntity : class, IEntity<TKey> 
        where TKey : struct
    {
        //add dal service
        services.AddScoped<IDataAccessService<TDalEntity, TKey>, DataAccessService<TDalEntity, TKey>>();

        //add bl service
        services.AddScoped<IRepository<TBllEntity, TKey>, Repository<TBllEntity, TDalEntity, TKey>>();
        services.AddScoped<IMailRepository, MailRepository>();
    }

    protected void AddCrudServices<TDalEntity, TBllEntity>(IServiceCollection services) 
        where TDalEntity : class, IEntity<Guid>
		where TBllEntity : class, IEntity<Guid>
    {
        AddCrudServices<TDalEntity, TBllEntity, Guid>(services);
    }
}