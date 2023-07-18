using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;

using Common.Enums;
using Common.Mapping;
using DAL.Entities.AuthCode;
using DAL.Entities.CommonConfigs;
using DAL.Entities.FAQ;
using DAL.Entities.KontragentIdentity;
using DAL.Entities.Order;
using DAL.Entities.Payment;
using DAL.Entities.PersonIdentity;
using DAL.Entities.Return;
using DAL.Entities.RoutePoint;
using DAL.Entities.Transport;
using DAL.Entities.User;
using DAL.Entities.News;
using DAL.Entities.Files;
using DAL.Entities.PassengerData;
using DAL.Entities.TripDraft;
using DAL.Entities.TripRoute;
using BE = BLL.Entities;

namespace BLL.Mappings.MapperFactory;

public class MapperFactory : MapperFactoryBase
{
    public static IMapper GetMapper() =>
        GetMapper(cfg =>
        {
            cfg.AddExpressionMapping();

            // Define the default AutoMapper configuration here
            cfg.CreateMap<User, BE.User.User>()
                .ForMember(r => r.Identities, opt => opt.ExplicitExpansion())
                .ReverseMap();


            cfg.CreateMap<Transport, BE.Transport.Transport>()
                .ForMember(r => r.Carrier, opt => opt.ExplicitExpansion())
                .ForMember(r => r.Driver, opt => opt.ExplicitExpansion())
                .ReverseMap();

            cfg.CreateMap<RoutePoint, BE.RoutePoint.RoutePoint>()
                .ReverseMap();

            cfg.CreateMap<Order, BE.Order.Order>()
                .ForMember(r => r.StartTripRoutePoint, opt => opt.ExplicitExpansion())
                .ForMember(r => r.EndTripRoutePoint, opt => opt.ExplicitExpansion())
                //.ForMember(r => r.SettlePoint, opt => opt.ExplicitExpansion())
                .ForMember(r => r.Client, opt => opt.ExplicitExpansion())
                //.ForMember(r => r.Carrier, opt => opt.ExplicitExpansion())
                .ForMember(r => r.Payment, opt => opt.ExplicitExpansion())
                .ReverseMap();

            cfg.CreateMap<OperatorOrder, BE.Order.OperatorOrder>()
                .ForMember(dest => dest.Operator, opt => opt.ExplicitExpansion())
                .ReverseMap();

            cfg.CreateMap<Return, BE.Return.Return>()
                .ReverseMap();

            cfg.CreateMap<Payment, BE.Payment.Payment>()
                .ForMember(r => r.Return, opt => opt.ExplicitExpansion())
                .ReverseMap();

            cfg.CreateMap<PersonIdentity, BE.PersonIdentity.PersonIdentity>()
                .ForMember(r => r.User, opt => opt.ExplicitExpansion())
                .ReverseMap();

            cfg.CreateMap<OperatorPersonIdentity, BE.PersonIdentity.OperatorPersonIdentity>()
                .ReverseMap();

            cfg.CreateMap<DriverPersonIdentity, BE.PersonIdentity.DriverPersonIdentity>()
                .ForMember(dest => dest.Trips, opt => opt.ExplicitExpansion())
                .ReverseMap();

            cfg.CreateMap<DispatcherPersonIdentity, BE.PersonIdentity.DispatcherPersonIdentity>()
                .ReverseMap();

            cfg.CreateMap<ClientPersonIdentity, BE.PersonIdentity.ClientPersonIdentity>()
                .ForMember(r => r.Orders, opt => opt.ExplicitExpansion())
                .ReverseMap();

            cfg.CreateMap<KontragentIdentity, BE.KontragentIdentity.KontragentIdentity>()
                .ReverseMap();

            cfg.CreateMap<CarrierIdentity, BE.KontragentIdentity.CarrierIdentity>()
                .ForMember(dest => dest.Drivers, opt => opt.ExplicitExpansion())
                .ForMember(dest => dest.Transports, opt => opt.ExplicitExpansion())
                .ForMember(dest => dest.Trips, opt => opt.ExplicitExpansion())
                .ReverseMap();

            cfg.CreateMap<InsuranceIdentity, BE.KontragentIdentity.InsuranceIdentity>()
                .ReverseMap();

            cfg.CreateMap<AuthCode, BE.AuthCode.AuthCode>()
                .ReverseMap();

            cfg.CreateMap<TripRoute, BE.TripRoute.TripRoute>()
                .ForMember(r => r.TripRoutePoints, opt => opt.ExplicitExpansion())
                .ForMember(r => r.PopularDestinationPicture, opt => opt.ExplicitExpansion())
                .ReverseMap();
            
            cfg.CreateMap<DAL.Entities.Trip.Trip, BE.Trip.Trip>()
                .ForMember(r => r.TripRoute, opt => opt.ExplicitExpansion())
                .ForMember(r => r.Driver, opt => opt.ExplicitExpansion())
                .ForMember(r => r.Carrier, opt => opt.ExplicitExpansion())
                .ForMember(r => r.Transport, opt => opt.ExplicitExpansion())
                .ReverseMap();

            cfg.CreateMap<TripRoutePoint, BE.TripRoute.TripRoutePoint>()
                .ForMember(r => r.TripRoute, opt => opt.ExplicitExpansion())
                .ForMember(r => r.RoutePoint, opt => opt.ExplicitExpansion())
                .ReverseMap();

			cfg.CreateMap<FAQ, BE.FAQ.FAQ>().ReverseMap();

            cfg.CreateMap<News, BE.News.News>().ReverseMap();

            cfg.CreateMap<FAQCategory, BE.FAQ.FAQCategory>().ReverseMap();

			cfg.CreateMap<DAL.Entities.Files.File, BE.File.File>().ReverseMap();
			cfg.CreateMap<Base64File, BE.File.Base64File>().ReverseMap();
			cfg.CreateMap<S3File, BE.File.S3File>().ReverseMap();
            cfg.CreateMap<CommonConfiguration, BE.CommonConfigs.CommonConfiguration>().ReverseMap();
            cfg.CreateMap<PassengerData, BE.PassengerData.PassengerData>().ReverseMap();

            cfg.CreateMap<TripDraft, BE.TripDraft.TripDraft>()
                .ForMember(r => r.Carrier, opt => opt.ExplicitExpansion())
                .ForMember(r => r.Driver, opt => opt.ExplicitExpansion())
                .ForMember(r => r.Transport, opt => opt.ExplicitExpansion())
                .ForMember(r=>r.TripRoute, opt => opt.ExplicitExpansion())
                .ReverseMap();
        });
}