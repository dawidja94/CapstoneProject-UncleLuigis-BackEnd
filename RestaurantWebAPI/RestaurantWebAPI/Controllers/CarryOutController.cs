using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantWebAPI.Data;
using RestaurantWebAPI.Models.Bodies;
using RestaurantWebAPI.Models.Entities;
using RestaurantWebAPI.Models.ServiceRequests;
using RestaurantWebAPI.Services;

namespace RestaurantWebAPI.Controllers
{

    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class CarryOutController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICarryOutService _carryOutService;
        
        public CarryOutController(ICarryOutService carryOutService, ApplicationDbContext context)
        {
            _carryOutService = carryOutService;
            _context = context;
        }

        //GET: CarryOut/GetAllOutsInCart
        [HttpGet("GetAllCarryOutsInCart/{id}")]
        [AllowAnonymous]
        public IActionResult GetAllCarryOutsInCart(int id)
        {
            var request = new GetAllCarryOutsInCartRequest()
            {
                CustomerId = id
            };

            var response = _carryOutService.GetAllCarryOutsInCart(request);

            if (response.IsSuccessful)
            {
                return Ok(response.CarryOuts);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        //GET: CarryOut/GetAllCarryOutsForCustomer/{id}
        [HttpGet("GetAllCarryOutsForCustomer/{id}")]
        public IActionResult GetAllCarryOutsForCustomer(int id)
        {
            var request = new GetAllCarryOutsForCustomerRequest() {
                CustomerId = id
            };
            var response = _carryOutService.GetAllCarryOutsForCustomer(request);

            if (response.IsSuccessful)
            {
                return Ok(response.CarryOuts);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
        //GET: CarryOut/GetCarryOutById/{id}
        [HttpGet("GetCarryOutById/{id}")]
        public IActionResult GetCarryOutById([FromRoute]int id)
        {
            var request = new GetCarryOutByIdRequest()
            {
                BundleId = id
            };

            var response = _carryOutService.GetCarryOutById(request);

            if (response.IsSuccessful)
            {
                return Ok(response.CarryOuts);
            }
            else
            {
                return BadRequest(response.Message);
            }

        }

        //GET: CarryOut/GetAllCarryOutsForDate
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
        public IActionResult CreateCarryOut([FromBody]CustomerBody body)
        {
            var request = new CreateCarryOutRequest
            {
                Customer = body
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

        // POST: CarryOut/AddToCart
        [HttpPost("AddToCart")]
        public IActionResult AddToCart(CarryOutBody body)
        {
            var request = new AddToCartRequest
            {
                CarryOutToAddToCart = body,
            };

            var response = _carryOutService.AddToCart(request);

            if (response.IsSuccessful)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: CarryOut/RemoveFromCart
        [HttpDelete("RemoveFromCart")]
        public IActionResult RemoveFromCart(RemoveCarryOutBody body)
        {
            var request = new RemoveFromCartRequest
            {
                CarryOutId = body.CarryOutId,
                CustomerId = body.CustomerId
            };

            var response = _carryOutService.RemoveFromCart(request);

            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        //PUT: CarryOut/UpdateCarryOut
        [HttpPut("UpdateCarryOut")]
        public IActionResult UpdateCarryOut(List<CarryOut> body)
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