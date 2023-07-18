using Common.Delta;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.Entities.Trip;
using PL.Entities.TripDraft;
using PL.MVC.Models.Api;
using PL.Services.Admin;
using PL.Services.Admin.Models;
using DayOfWeek=Common.Enums.DayOfWeek;
using System.Globalization;

namespace PL.MVC.Controllers.Api;
[ApiController]
[Authorize]
[Route("/api/drafts")]
public class TripDraftsApiController:ControllerBase
{
    private readonly TripDraftsService _drafts;
    private readonly TransportsService _transports;
    private readonly TripsService _trips;
	private readonly TripRoutesService _tripRoutesService;

    public TripDraftsApiController(TripDraftsService drafts, TransportsService transports, TripsService trips, TripRoutesService tripRoutesService)
    {
        _drafts = drafts;
        _transports = transports;
        _trips = trips;
        _tripRoutesService = tripRoutesService;
    }

	[HttpPost]
	[Route("delete")]
    [AllowAnonymous]
	public async Task<IActionResult> DeleteDraft([FromBody] Guid id)
	{
		await _drafts.DeleteAsync(id);

		return Ok();
	}

	[HttpPost]
    [AllowAnonymous]
    [Route("update")]
    public async Task<IActionResult> UpdateDrafts([FromBody] List<TripDraftApiModel> items)
    {
        items.ForEach(r => { r.Id ??= Guid.NewGuid(); });

        var existDrafts = await _drafts
            .GetByExistingKeys(items.Select(r => r.Id.Value));

        var triples =
            items.DistinctBy(r => r.Id).Select(r => r.ToPlModel())
                .ToDictionary<TripDraft, Guid, (TripDraft, TripDraft?)>(r => r.Id,
                    f => (f, existDrafts.Items.FirstOrDefault(e => e.Id == f.Id)));

        var results = new List<TripDraft>();
         foreach (var triple in triples)
         {
             var id = triple.Key;
             var nov = triple.Value.Item1;
             var old = triple.Value.Item2;
            var tripRoute = await _tripRoutesService.GetByIdAsync(nov.TripRouteId);
            var minutes = tripRoute.TripRoutePoints.Sum(x => x.MinutesFromStart);

			 nov.EndTimeOfDay = 
                DateTime.ParseExact(nov.StartTimeOfDay, "HH:mm:ss",
										CultureInfo.InvariantCulture).AddMinutes(minutes).ToString("HH:mm:ss");
            nov.EndDayOfWeek = nov.StartDayOfWeek;
            if (minutes >= 1440 
                || 
                DateTime.ParseExact(nov.StartTimeOfDay, "HH:mm:ss",CultureInfo.InvariantCulture) 
                >
                DateTime.ParseExact(nov.EndTimeOfDay, "HH:mm:ss",CultureInfo.InvariantCulture))
			{
                var additionalDays = Math.Floor(minutes / 1440.0);

				var needDay = (int)nov.StartDayOfWeek + 1 + additionalDays;
                while (needDay > 6)
                {
                    needDay = needDay - 6;
                }
				nov.EndDayOfWeek = (Common.Enums.DayOfWeek)(needDay);
			}

			if (old != null)
             {
                 // old.TransportId = nov.TransportId;
                 // old.CarrierId = nov.CarrierId;
                 // old.DriverId = nov.DriverId;
                 // old.TripRouteId = nov.TripRouteId;
                 // old.StartDayOfWeek = nov.StartDayOfWeek;
                 // old.EndDayOfWeek = nov.EndDayOfWeek;
                 // old.StartTimeOfDay = nov.StartTimeOfDay;
                 // old.EndTimeOfDay = nov.EndTimeOfDay;

                 results.Add(await _drafts.PatchAsync(id, new Delta<TripDraft>()
                 {
                     {nameof(TripDraft.TransportId), nov.TransportId},
                     {nameof(TripDraft.CarrierId), nov.CarrierId},
                     {nameof(TripDraft.DriverId), nov.DriverId},
                     {nameof(TripDraft.TripRouteId), nov.TripRouteId},
                     {nameof(TripDraft.StartDayOfWeek), nov.StartDayOfWeek},
					 {nameof(TripDraft.StartTimeOfDay), nov.StartTimeOfDay},
					 {nameof(TripDraft.EndDayOfWeek), nov.EndDayOfWeek},
                     {nameof(TripDraft.EndTimeOfDay), nov.EndTimeOfDay},
                 }));
                 
                 continue;
             }

             nov.Id = id;
             nov.CreatedAt = DateTime.UtcNow.AddHours(3);
             results.Add(await _drafts.AddAsync(nov));

         }
         
         return Ok(results);
    }

    private async Task<List<TripDraft>> updateDrafts(List<TripDraftApiModel> items)
    {
        items.ForEach(r => { r.Id ??= Guid.NewGuid(); });

        var existDrafts = await _drafts
            .GetByExistingKeys(items.Select(r => r.Id.Value));

        var triples =
            items.DistinctBy(r => r.Id).Select(r => r.ToPlModel())
                .ToDictionary<TripDraft, Guid, (TripDraft, TripDraft?)>(r => r.Id,
                    f => (f, existDrafts.Items.FirstOrDefault(e => e.Id == f.Id)));

        var results = new List<TripDraft>();
        foreach (var triple in triples)
        {
            var id = triple.Key;
            var nov = triple.Value.Item1;
            var old = triple.Value.Item2;

            if (old != null)
            {
                results.Add(await _drafts.PatchAsync(id, new Delta<TripDraft>()
                {
                    {nameof(TripDraft.TransportId), nov.TransportId},
                    {nameof(TripDraft.CarrierId), nov.CarrierId},
                    {nameof(TripDraft.DriverId), nov.DriverId},
                    {nameof(TripDraft.TripRouteId), nov.TripRouteId},
                    {nameof(TripDraft.StartDayOfWeek), nov.StartDayOfWeek},
                    {nameof(TripDraft.EndDayOfWeek), nov.EndDayOfWeek},
                    {nameof(TripDraft.EndTimeOfDay), nov.EndTimeOfDay},
                    {nameof(TripDraft.Price),nov.Price}
                }));

                continue;
            }

            nov.Id = id;
            nov.CreatedAt = DateTime.UtcNow.AddHours(3);
            results.Add(await _drafts.AddAsync(nov));
        }

       


        return results;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("prolongate/{days}")]
    public async Task<IActionResult> Prolongate([FromRoute]int days,[FromBody] List<TripDraftApiModel> items)
    {
        var tripsToCreate = new List<Trip>();
        var updatedDrafts=await updateDrafts(items);
        var currentDate = DateTime.UtcNow.AddHours(3);

        var currentDayOfWeek = (DayOfWeek) (int)((int)currentDate.DayOfWeek - 1 ==-1?0:currentDate.DayOfWeek - 1);

        var transports = await _transports.GetAsync(new FilterModel()
        {
            ExistKeys = updatedDrafts.Where(r => r.TransportId != null).Select(r => r.TransportId.Value).ToList()
        });

        updatedDrafts.ForEach(draft =>
        {
            var differenceOnWeek = draft.StartDayOfWeek - currentDayOfWeek;
            var startDateOnThisWeek = currentDate.AddDays(differenceOnWeek);
            var startTime = TimeSpan.Parse(draft.StartTimeOfDay ?? string.Empty);

            var departureDateTime = startDateOnThisWeek.Date.AddDays(days)
           .AddHours(startTime.Hours)
           .AddMinutes(startTime.Minutes);
    
            var trip = new Trip
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow.AddHours(3),
                IsDeleted = draft.IsDeleted,
                DeletedAt = draft.DeletedAt,
                DepartureTime = departureDateTime,
                Price = draft.Price,
                CarrierId = draft.CarrierId,
                TransportId = draft.TransportId,
                DriverId = draft.DriverId,
                TripRouteId = draft.TripRouteId,
                SeatsCount = transports.Items.FirstOrDefault(r=>r.Id==draft.TransportId)?.Seats??0,
            };
            
            tripsToCreate.Add(trip);
        });
        
        await _trips.UpdateRangeAsync(tripsToCreate);

        var result = await _trips.GetAsync(new FilterModel()
        {
            ExistKeys = tripsToCreate.Select(r => r.Id).ToList()
        });

        return Ok(result);
    }

}