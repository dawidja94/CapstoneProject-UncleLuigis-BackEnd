using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantWebAPI.Models.Entities;
using RestaurantWebAPI.Models.ServiceRequests;
using RestaurantWebAPI.Services;

namespace RestaurantWebAPI.Controllers
{
 
    [ApiController]
    [Route("api/[controller]")]
    public class CarryOutController : ControllerBase
    {
        private readonly ICarryOutService _carryOutService;
        public CarryOutController(ICarryOutService carryOutService)
        {
            _carryOutService = carryOutService;
        }

        //GET: CarryOut/GetAllOuts
        [HttpGet("GetAllCarryOuts")]
        public IActionResult GetAllCarryOuts()
        {
            var request = new GetAllCarryOutsForDateRequest();
            var response = _carryOutService.GetAllCarryOutsForDate(request);

            if (response.IsSuccessful)
            {
                return Ok(response.CarryOuts);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
        //GET: CarryOut/GetAllOutsForDate
        [HttpGet("GetAllCarryOutsForDate")]
        public IActionResult GetAllCarryOutsForDate()
        {
            var request = new GetAllCarryOutsForDateRequest();
            var response = _carryOutService.GetAllCarryOutsForDate(request);

            if (response.IsSuccessful)
            {
                return Ok(response.CarryOuts);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        //POST: CarryOut/CreateCarryOut
        [HttpPost("CreateCarryOut")]
        public IActionResult CreateCarryOut(List<CarryOut> body)
        {
            var request = new CreateCarryOutRequest
            {
                CarryOut = body
            };

            var response = _carryOutService.CreateCarryOut(request);

            if (response.IsSuccessful)
            {
                return Ok();
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        //PUT: CarryOut/UpdateCarryOut
        [HttpPut("UpdateCarryOut")]
        public IActionResult UpdateCarryOut (List<CarryOut> body)
        {
            var request = new UpdateCarryOutRequest
            {
                CarryOutToUpdate = body
            };

            var response = _carryOutService.UpdateCarryOut(request);

            if (response.IsSuccessful)
            {
                return Ok();
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
    }
}