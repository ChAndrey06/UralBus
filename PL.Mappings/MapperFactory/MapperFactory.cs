using AutoMapper;
using BLL.Entities.AuthCode;
using BLL.Entities.FAQ;
using BLL.Entities.KontragentIdentity;
using BLL.Entities.Order;
using BLL.Entities.Payment;
using BLL.Entities.PersonIdentity;
using BLL.Entities.Return;
using BLL.Entities.RoutePoint;
using BLL.Entities.Transport;
using BLL.Entities.TripRoute;
using BLL.Entities.User;
using BLL.Entities.News;
using BLL.Entities.Trip;
using Common.Enums;
using Common.Mapping;

using PE = PL.Entities;
using BLL.Entities.File;
using BLL.Entities.CommonConfigs;
using BLL.Entities.PassengerData;
using BLL.Entities.TripDraft;

namespace PL.Mappings.MapperFactory;

public class MapperFactory : MapperFactoryBase
{
    public static IMapper GetMapper() =>
        GetMapper(cfg =>
        {
            cfg.CreateMap<User, PE.User.User>().ReverseMap();

            cfg.CreateMap<Transport, PE.Transport.Transport>().ReverseMap();

            cfg.CreateMap<RoutePoint, PE.RoutePoint.RoutePoint>().ReverseMap();

            cfg.CreateMap<Return, PE.RoutePoint.RoutePoint>().ReverseMap();

            cfg.CreateMap<PersonIdentity, PE.PersonIdentity.PersonIdentity>()
                .ForMember(r => r.IdentityType, opt => opt.MapFrom(s => s.Discriminator.ConvertToEnum<PersonIdentityType>()))
                .ReverseMap()
                .ForMember(r => r.Discriminator, opt => opt.MapFrom(s => s.IdentityType.ToString()));

            cfg.CreateMap<OperatorPersonIdentity, PE.PersonIdentity.OperatorPersonIdentity>().ReverseMap();

            cfg.CreateMap<DriverPersonIdentity, PE.PersonIdentity.DriverPersonIdentity>().ReverseMap();

            cfg.CreateMap<DispatcherPersonIdentity, PE.PersonIdentity.DispatcherPersonIdentity>().ReverseMap();

            cfg.CreateMap<ClientPersonIdentity, PE.PersonIdentity.ClientPersonIdentity>().ReverseMap();

            cfg.CreateMap<Payment, PE.Payment.Payment>().ReverseMap();

            cfg.CreateMap<Order, PE.Order.Order>().ReverseMap();

            cfg.CreateMap<OperatorOrder, PE.Order.OperatorOrder>().ReverseMap();

            cfg.CreateMap<KontragentIdentity, PE.KontragentIdentity.KontragentIdentity>().ReverseMap();

            cfg.CreateMap<InsuranceIdentity, PE.KontragentIdentity.InsuranceIdentity>().ReverseMap();

            cfg.CreateMap<CarrierIdentity, PE.KontragentIdentity.CarrierIdentity>().ReverseMap();

            cfg.CreateMap<AuthCode, PE.AuthCode.AuthCode>().ReverseMap();

            cfg.CreateMap<User, PE.User.User>().ReverseMap();

            cfg.CreateMap<TripRoute, PE.TripRoute.TripRoute>().ReverseMap();

            cfg.CreateMap<Trip, PE.Trip.Trip>().ReverseMap();

            cfg.CreateMap<TripRoutePoint, Entities.TripRoute.TripRoutePoint>().ReverseMap();

            cfg.CreateMap<FAQ, PE.FAQ.FAQ>().ReverseMap();

            cfg.CreateMap<News, PE.News.News>().ReverseMap();

            cfg.CreateMap<FAQCategory, PE.FAQ.FAQCategory>().ReverseMap();

            cfg.CreateMap<BLL.Entities.File.File, PE.File.File>().ReverseMap();
            cfg.CreateMap<Base64File, PE.File.Base64File>().ReverseMap();
            cfg.CreateMap<S3File, PE.File.S3File>().ReverseMap();
            cfg.CreateMap<CommonConfiguration, PE.CommonConfigs.CommonConfiguration>().ReverseMap();
            
            cfg.CreateMap<PassengerData, PE.PassengerData.PassengerData>().ReverseMap();

            cfg.CreateMap<TripDraft, PE.TripDraft.TripDraft>()
                .ReverseMap();
        });
}