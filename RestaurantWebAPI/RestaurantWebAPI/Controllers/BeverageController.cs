using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantWebAPI.Models.Entities;
using RestaurantWebAPI.Models.ServiceRequests;
using RestaurantWebAPI.Services;

namespace RestaurantWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BeverageController : ControllerBase
    {
        private readonly IBeverageService _beverageservice;
        public BeverageController(IBeverageService beverageService)
        {
            _beverageservice = beverageService;
        }

        //GET: Beverage/GetAllBeverage
        // Tested and verified logic 02/08/2020 - Dawid
        [HttpGet("GetAllBeverage")]
        public IActionResult GetAllBeverage()
        {
            var request = new GetAllBeverageItemsRequest();
            var response = _beverageservice.GetAllBeverageItems(request);

            if (response.IsSuccessful)
            {
                return Ok(response.BeverageItems);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        //POST: Beverage/CreateBeverageItem
        // Tested and verified logic 02/08/2020 - Dawid
        [HttpPost("CreateBeverageItem")]
        [Authorize]
        public IActionResult CreateBeverageItem(Beverage body)
        {
            var request = new CreateBeverageItemRequest
            {
                BeverageItem = body,
            };

            var response = _beverageservice.CreateBeverageItem(request);

            if (response.IsSuccessful)
            {
                return Ok();
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        //PUT: Beverage/UpdateBeverageItem
        // Tested and verified logic 02/08/2020 - Dawid
        [HttpPut("UpdateBeverageItem")]
        [Authorize]
        public IActionResult UpdateBeverageItem(Beverage body)
        {
            var request = new UpdateBeverageItemRequest
            {
                BeverageItemToUpdate = body
            };

            var response = _beverageservice.UpdateBeverageItem(request);

            if (response.IsSuccessful)
            {
                return Ok();
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        //DELETE : Beverage/DeleteBeverageItem
        // Tested and verified logic 02/08/2020 - Dawid
        [HttpDelete("DeleteBeverageItem/{id}")]
        [Authorize]
        public IActionResult DeleteBeverageItem([FromRoute] int id)
        {
            var request = new DeleteBeverageItemRequest
            {
                Id = id
            };

            var response = _beverageservice.DeleteBeverageItem(request);

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