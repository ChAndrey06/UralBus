using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Entities.Order;
using BLL.Interfaces;
using Common.Models;
using DAL.Entities.Order;
using Microsoft.EntityFrameworkCore;
using PL.Services.Admin.Models;
using PL.Services.Client.Models;
using Order = PL.Entities.Order.Order;

namespace PL.Services.Admin
{
    public class OrdersService : BasePLAdminService<BLL.Entities.Order.Order, Order, FilterModel, Guid>
    {
        public OrdersService(IRepository<BLL.Entities.Order.Order, Guid> repository) : base(repository)
        {
        }

        public override async Task<GetWrapper<IEnumerable<Order>>> GetAsync(FilterModel filter)
        {
            var bllOrderGetWrapper = await _repository.GetAsync(
                limit: filter.Limit,
                offset: filter.Offset,
                includeProperties: new[]
                {
                     nameof(Order.Trip),
                    $"{nameof(Order.Trip)}.{nameof(Order.Trip.TripRoute)}",
                    nameof(Order.StartTripRoutePoint),
                    nameof(Order.EndTripRoutePoint),
                    nameof(Order.Client),
                    $"{nameof(Order.Client)}.{nameof(Order.Client.User)}"
                },
                filter: o => string.IsNullOrEmpty(filter.SearchQuery) 
                     || o.Trip.TripRoute.Title.Contains(filter.SearchQuery)
                     || o.Client.User.FirstName.Contains(filter.SearchQuery) 
                     || o.Client.User.LastName.Contains(filter.SearchQuery) 
                     || o.Client.User.Patronymic.Contains(filter.SearchQuery)
                     || o.Client.User.Email.Contains(filter.SearchQuery)
                     || o.Client.User.PhoneNumber.Contains(filter.SearchQuery)
                     || o.Id.ToString().Contains(filter.SearchQuery)
            );

            var plOrderGetWrapper = new GetWrapper<IEnumerable<Order>>(
                _mapper.Map<IEnumerable<Order>>(bllOrderGetWrapper.Items.OrderByDescending(o => o.CreatedAt)),
                bllOrderGetWrapper.TotalCount
            );

            return plOrderGetWrapper;
        }

        public async Task<GetWrapper<IEnumerable<Order>>> GetAsync(MyTripsFilterModel filter, Guid? clientId)
        {
            var bllOrderGetWrapper = await _repository.GetAsync(
                limit: filter.Limit,
                offset: filter.Offset,
                filter: o => 
                    (string.IsNullOrEmpty(filter.SearchQuery) || o.Trip.TripRoute.Title.Contains(filter.SearchQuery)) &&
                    (clientId == null || o.ClientId == clientId)
                ,
                includeProperties: new[]
                {
                     nameof(Order.Trip),
                    $"{nameof(Order.Trip)}.{nameof(Order.Trip.TripRoute)}",
                    nameof(Order.StartTripRoutePoint),
                    "StartTripRoutePoint.RoutePoint",
                    nameof(Order.EndTripRoutePoint),
                    "EndTripRoutePoint.RoutePoint",
                    nameof(Order.Client),
                    $"{nameof(Order.Client)}.{nameof(Order.Client.User)}"
                }
            );

            var plOrderGetWrapper = new GetWrapper<IEnumerable<Order>>(
                _mapper.Map<IEnumerable<Order>>(bllOrderGetWrapper.Items),
                bllOrderGetWrapper.TotalCount
            );

            if (filter.Period != null)
            {
                switch (filter.Period)
                {
                    case "past":
                        plOrderGetWrapper.Items = plOrderGetWrapper.Items.Where(r => r.ArrivalTime < DateTime.Now);
                        break;
                    case "upcoming":
                        plOrderGetWrapper.Items = plOrderGetWrapper.Items.Where(r => r.ArrivalTime >= DateTime.Now);
                        break;
                }
            }
            
            if (filter.OrderByModel != null)
            {
                switch (filter.OrderByModel.PropertyName)
                {
                    case nameof(PL.Entities.Trip.Trip.DepartureTime):
                        plOrderGetWrapper.Items = filter.OrderByModel.Direction == OrderByDirection.Asc
                            ? plOrderGetWrapper.Items.OrderBy(r => r.DepartureTime)
                            : plOrderGetWrapper.Items.OrderByDescending(r => r.DepartureTime);
                        break;
                    case nameof(PL.Entities.Trip.Trip.ArrivalTime):
                        plOrderGetWrapper.Items = filter.OrderByModel.Direction == OrderByDirection.Asc
                            ? plOrderGetWrapper.Items.OrderBy(r => r.ArrivalTime)
                            : plOrderGetWrapper.Items.OrderByDescending(r => r.ArrivalTime);
                        break;
                    case nameof(PL.Entities.Trip.Trip.Price):
                        plOrderGetWrapper.Items = filter.OrderByModel.Direction == OrderByDirection.Asc
                            ? plOrderGetWrapper.Items.OrderBy(r => r.Price)
                            : plOrderGetWrapper.Items.OrderByDescending(r => r.Price);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            return plOrderGetWrapper;
        }
        
        public async Task<GetWrapper<IEnumerable<Order>>> GetWithRefundParamsAsync(FilterModel filter, bool refund = false)
        {
            var bllOrderGetWrapper = await _repository.GetAsync(
                limit: filter.Limit,
                offset: filter.Offset,
                includeProperties: new[]
                {
                     nameof(Order.Trip),
                    $"{nameof(Order.Trip)}.{nameof(Order.Trip.TripRoute)}",
                    nameof(Order.StartTripRoutePoint),
                    nameof(Order.EndTripRoutePoint),
                    nameof(Order.Client),
                    $"{nameof(Order.Client)}.{nameof(Order.Client.User)}"
                },
                filter: o => 
                    refund ? o.Status.StartsWith("Refund") : !o.Status.StartsWith("Refund")
                    && (string.IsNullOrEmpty(filter.SearchQuery) 
                        || o.Trip.TripRoute.Title.Contains(filter.SearchQuery)
                        || o.Client.User.FirstName.Contains(filter.SearchQuery) 
                        || o.Client.User.LastName.Contains(filter.SearchQuery) 
                        || o.Client.User.Patronymic.Contains(filter.SearchQuery)
                        || o.Client.User.Email.Contains(filter.SearchQuery)
                        || o.Client.User.PhoneNumber.Contains(filter.SearchQuery)
                        || o.Id.ToString().Contains(filter.SearchQuery))
            );

            var plOrderGetWrapper = new GetWrapper<IEnumerable<Order>>(
                _mapper.Map<IEnumerable<Order>>(bllOrderGetWrapper.Items.OrderByDescending(o => o.CreatedAt)),
                bllOrderGetWrapper.TotalCount
            );

            return plOrderGetWrapper;
        }
        
        public override async Task<Order?> GetByIdAsync(Guid id)
        {
            var bllOrder = await _repository.GetByIdAsync(id, includeProperties: new[]
            {
                "Trip",
                "Trip.TripRoute",
                "StartTripRoutePoint",
                "EndTripRoutePoint",
                "Client",
                "Client.User"
            });

            return _mapper.Map<Order>(bllOrder);
        }
    }
}