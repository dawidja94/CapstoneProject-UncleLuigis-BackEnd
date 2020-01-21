using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        [HttpGet("read")]
        public IActionResult GetAllFood()
        {
            var items = _foodservice.GetAllFoodMenuItems();

            if (items != null) 
            {
                return Ok(items);
            }
            else
            {
                return BadRequest("You suck");
            }
        }
    }
}
