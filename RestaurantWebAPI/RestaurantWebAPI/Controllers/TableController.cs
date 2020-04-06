using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        // POST: Table/ReserveTable
        [Authorize]
        [HttpPost("ReserveTable")]
        public IActionResult ReserveTable(ReserveTableBody body)
        {
            var request = new CreateTableReservationRequest
            {
                CustomerId = body.CustomerId,
                PartySize = body.PartySize,
                TableId = body.TableId
            };

            var response = _tableService.CreateTableReservation(request);

            if (response.IsSuccessful)
            {
                return Ok(response.Reservation);
            }
            else
            {
                return BadRequest();
            }
        }

        // GET: Table/GetCustomerReservations/{id}
        [HttpGet("GetCustomerReservations/{id}")]
        public IActionResult GetCustomerReservations([FromRoute] int id)
        {
            var request = new GetTableReservationsByCustomerRequest
            {
                CustomerId = id
            };

            var response = _tableService.GetTableReservationsByCustomer(request);

            if (response.IsSuccessful)
            {
                return Ok(response.Reservations);
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: Table/GetReservation
        [HttpPost("GetReservation")]
        public IActionResult GetReservation(ReserveTableBody body)
        {
            var request = new GetReservationRequest
            {
                ReservationId = body.TableId,
                CustomerId = body.CustomerId
            };

            var response = _tableService.GetReservation(request);

            if (response.IsSuccessful)
            {
                return Ok(response.TableReservation);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT: Table/CancelReservation
        [HttpPut("CancelReservation")]
        [Authorize]
        public IActionResult CancelReservation(ReserveTableBody body)
        {
            var request = new UpdateTableReservationRequest
            {
                CustomerId = body.CustomerId,
                ReservationId = body.TableId
            };

            var response = _tableService.UpdateTableReservation(request);

            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}