using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantWebAPI.Models.Entities;
using RestaurantWebAPI.Models.ServiceRequests;
using RestaurantWebAPI.Services;

namespace RestaurantWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FoodController : ControllerBase
    {
        private readonly IFoodService _foodservice;
        public FoodController(IFoodService foodService)
        {
            _foodservice = foodService;
        }

        // GET: Food/GetAllFood
        // Tested and verified logic 1/28/2020 - Thomas
        [HttpGet("GetAllFood")]
        public IActionResult GetAllFood()
        {
            var request = new GetAllFoodMenuItemsRequest();
            var response = _foodservice.GetAllFoodMenuItems(request);

            if (response.IsSuccessful) 
            {
                return Ok(response.FoodItems);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        // POST: Food/CreateFoodItem
        [HttpPost("CreateFoodItem")]
        public IActionResult CreateFoodItem(Food body)
        {
            var request = new CreateFoodItemRequest
            {
                FoodItem = body
            };

            var response = _foodservice.CreateFoodItem(request);

            if (response.IsSuccessful)
            {
                return Ok();
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        // PUT: Food/UpdateFoodItem
        [HttpPut("UpdateFoodItem")]
        public IActionResult UpdateFoodItem(Food body)
        {
            var request = new UpdateFoodItemRequest
            {
                FoodToUpdate = body
            };

            var response = _foodservice.UpdateFoodItem(request);

            if (response.IsSuccessful)
            {
                return Ok();
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
        // DELETE: Food/DeleteFoodItem
        [HttpDelete("DeleteFoodItem/{id}")]
        public IActionResult DeleteFoodItem([FromRoute]int id)
        {
            var request = new DeleteFoodItemRequest
            {
                Id = id
            };
            var response = _foodservice.DeleteFoodItem(request);

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
