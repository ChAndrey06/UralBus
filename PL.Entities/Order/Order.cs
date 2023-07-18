using Common.Enums;
using Common.Helpers;
using PL.Entities.PersonIdentity;
using PL.Entities.TripRoute;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PL.Entities.Order;
public class Order : BaseEntity
{
    [Required(ErrorMessage = "Поле 'Количество взрослых' является обязательным.")]
    public int AdultsCount { get; set; }

    [Required(ErrorMessage = "Поле 'Количество детей' является обязательным.")]
    public int ChildrenCount { get; set; }

    [JsonIgnore]
    public string StatusString { get; set; }

    [JsonIgnore]
    public string CreationTypeString { get; set; }

    [JsonIgnore]
    [Required(ErrorMessage = "Поле 'Причина возврата' является обязательным.")]
    public string? ReasonRefund { get; set; }

    public decimal Discount { get; set; }

    [Required(ErrorMessage = "Поле 'Маршрут' является обязательным.")]
    public Guid TripId { get; set; }

    [Required(ErrorMessage = "Поле 'Начальная точка маршрута' является обязательным.")]
    public Guid StartTripRoutePointId { get; set; }

    [Required(ErrorMessage = "Поле 'Конечная точка маршрута' является обязательным.")]
    public Guid EndTripRoutePointId { get; set; }
    
    
    public Guid ClientId { get; set; }

    [Required(ErrorMessage = "Поле 'Клиент' является обязательным.")]
    public string ClientPhoneNumber { get; set; }
    public string? ClientFirstName { get; set; }
    public string? ClientLastName { get; set; }
    public string? ClientPatronymic { get; set; }

    public Guid? PaymentId { get; set; }

    public Trip.Trip Trip { get; set; }

    public TripRoutePoint StartTripRoutePoint { get; set; }

    public TripRoutePoint EndTripRoutePoint { get; set; }

    public ClientPersonIdentity Client { get; set; }

    public Payment.Payment? Payment { get; set; }

    [Required(ErrorMessage = "Поле 'Статус' является обязательным.")]
    public OrderStatus Status
    {
        get => StatusString != null ? StatusString.ParseEnum<OrderStatus>() : default;
        set => StatusString = value.ToString();
    }

    [Required(ErrorMessage = "Поле 'Тип создания' является обязательным.")]
    public OrderCreationType CreationType
    {
        get => CreationTypeString != null ? CreationTypeString.ParseEnum<OrderCreationType>() : default;
        set => CreationTypeString = value.ToString();
    }
    public decimal? Price => Trip is null ? null : (Trip.Price + Trip.TripRoute.TripRoutePoints.Sum(r => r.PriceForPoint)) * (AdultsCount + ChildrenCount);
    public decimal? TotalPrice => Trip is null ? null : (Trip.Price + Trip.TripRoute.TripRoutePoints.Sum(r => r.PriceForPoint) - Discount) * (AdultsCount + ChildrenCount * 0.8M);

    public DateTime? DepartureTime => Trip is null ? null : Trip.DepartureTime.AddMinutes(StartTripRoutePoint.MinutesFromStart);
    public DateTime? ArrivalTime => Trip is null ? null :  Trip.DepartureTime.AddMinutes(EndTripRoutePoint.MinutesFromStart);
}