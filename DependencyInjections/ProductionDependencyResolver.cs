using Common.Services;
using Common.Services.SmsService;
using DAL.Entities.AuthCode;
using DAL.Entities.KontragentIdentity;
using DAL.Entities.Order;
using DAL.Entities.Payment;
using DAL.Entities.PersonIdentity;
using DAL.Entities.Return;
using DAL.Entities.RoutePoint;
using DAL.Entities.Transport;
using DAL.Entities.User;
using DAL.Entities.FAQ;
using DAL.Entities.News;
using DAL.Interfaces;
using DAL.SQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PL.Services.Admin;
using PL.Services.Client;
using PL.Services.Interfaces.UserService;
using PL.Services.UserService;

namespace DependencyInjections;

public class ProductionDependencyResolver : BaseResolver
{
	public ProductionDependencyResolver(IConfiguration configuration) : base(configuration)
	{
	}


	public override void RegisterBlServices(IServiceCollection services)
	{
		//   services.AddScoped<IRepository<BE.User.User,Guid>, Repository<BE.User.User,DE.User.User,Guid>>();
	}
    public override void RegisterDalServices(IServiceCollection services)
    {
        services.AddScoped<IDatabaseConnection,PgDatabaseConnection>();
        
        services.AddScoped<IDataAccessService<User,Guid>, DataAccessService<User,Guid>>();

        services.AddScoped<IDataAccessService<PL.Entities.User.User, Guid>, DataAccessService<PL.Entities.User.User, Guid>>();
    }

    

	public override void RegisterPlServices(IServiceCollection services)
	{
		services.AddScoped<IUserService, UserService>();
		services.AddScoped<UsersService>();
		services.AddScoped<AuthCodesService>();
		services.AddScoped<PL.Services.Admin.RoutePointsService>();
		services.AddScoped<PL.Services.Client.RoutePointsService>();
		services.AddScoped<PL.Services.Admin.TripRoutesService>();
		services.AddScoped<PL.Services.Admin.TripsService>();
		services.AddScoped<PL.Services.Admin.TripRoutePointsService>();
		services.AddScoped<PL.Services.Admin.FAQsService>();
		services.AddScoped<PL.Services.Admin.CommonConfigsService>();
		services.AddScoped<PL.Services.Admin.NewsService>();
		services.AddScoped<PL.Services.Admin.FAQCategoriesService>();
		services.AddScoped<PL.Services.Client.TripsService>();
		services.AddScoped<PL.Services.Client.FAQsService>();
		services.AddScoped<PL.Services.Client.NewsService>();
		services.AddScoped<PL.Services.Admin.TransportsService>();
		services.AddScoped<PL.Services.Admin.KontragentsService>();
		services.AddScoped<PL.Services.Admin.DriversService>();
		services.AddScoped<PL.Services.Admin.PassengersDataService>();
		services.AddScoped<PopularDestinationsService>();
		services.AddScoped<OrdersService>();
		services.AddScoped<PL.Services.UserService.UserService>();
		
		services.AddScoped<TripDraftsService>();
	}

	public override void RegisterCommonServices(IServiceCollection services)
	{
		services.AddScoped<ISmsService, SmsService>();
	}


    public override void RegisterServices(IServiceCollection services)
    {
        base.RegisterServices(services);
        
        AddCrudServices<User,BLL.Entities.User.User>(services);
        AddCrudServices<Transport,BLL.Entities.Transport.Transport>(services);
        AddCrudServices<RoutePoint,BLL.Entities.RoutePoint.RoutePoint>(services);
        AddCrudServices<Return,BLL.Entities.Return.Return>(services);
        AddCrudServices<PersonIdentity,BLL.Entities.PersonIdentity.PersonIdentity>(services);
        AddCrudServices<OperatorPersonIdentity,BLL.Entities.PersonIdentity.OperatorPersonIdentity>(services);
        AddCrudServices<DriverPersonIdentity,BLL.Entities.PersonIdentity.DriverPersonIdentity>(services);
        AddCrudServices<DispatcherPersonIdentity,BLL.Entities.PersonIdentity.DispatcherPersonIdentity>(services);
        AddCrudServices<ClientPersonIdentity,BLL.Entities.PersonIdentity.ClientPersonIdentity>(services);
        AddCrudServices<Payment,BLL.Entities.Payment.Payment>(services);
        AddCrudServices<Order,BLL.Entities.Order.Order>(services);
        AddCrudServices<OperatorOrder,BLL.Entities.Order.OperatorOrder>(services);
        AddCrudServices<KontragentIdentity,BLL.Entities.KontragentIdentity.KontragentIdentity>(services);
        AddCrudServices<InsuranceIdentity,BLL.Entities.KontragentIdentity.InsuranceIdentity>(services);
        AddCrudServices<CarrierIdentity, BLL.Entities.KontragentIdentity.CarrierIdentity>(services);
        AddCrudServices<AuthCode, BLL.Entities.AuthCode.AuthCode>(services);
        AddCrudServices<DAL.Entities.TripRoute.TripRoute, BLL.Entities.TripRoute.TripRoute>(services);
        AddCrudServices<DAL.Entities.TripRoute.TripRoutePoint, BLL.Entities.TripRoute.TripRoutePoint>(services);
        AddCrudServices<FAQ, BLL.Entities.FAQ.FAQ>(services);
        AddCrudServices<News, BLL.Entities.News.News>(services);
		AddCrudServices<DAL.Entities.CommonConfigs.CommonConfiguration, BLL.Entities.CommonConfigs.CommonConfiguration>(services);
        AddCrudServices<FAQCategory, BLL.Entities.FAQ.FAQCategory>(services);
        AddCrudServices<DAL.Entities.Trip.Trip,BLL.Entities.Trip.Trip>(services);
		AddCrudServices<DAL.Entities.Files.Base64File, BLL.Entities.File.Base64File>(services);
		AddCrudServices<DAL.Entities.PassengerData.PassengerData, BLL.Entities.PassengerData.PassengerData>(services);
		AddCrudServices<DAL.Entities.TripDraft.TripDraft,BLL.Entities.TripDraft.TripDraft>(services);
    }
}