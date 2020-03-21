using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantWebAPI.Models.Bodies;
using RestaurantWebAPI.Models.ServiceRequests;
using RestaurantWebAPI.Services;

namespace RestaurantWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        // POST: Table/GetAvailableTables
        [HttpPost("GetAvailableTables")]
        public IActionResult GetAvailableTables(TableBody body)
        {
            var request = new GetAvailableTableReservationsRequest
            {
                PartySize = body.PartySize,
                ReservationDate = body.ReservationDate,
                TimeSlot = body.TimeSlot
            };

            var response = _tableService.GetAvailableTableReservations(request);

            if (response.IsSuccessful)
            {
                return Ok(response.Reservations);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}