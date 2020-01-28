using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantWebAPI.Models.Entities;
using RestaurantWebAPI.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        //POST : // GET: Customer/CreateCustomer
        [HttpPost("CreateCustomer")]
        public IActionResult CreateCustomer(Customer body)
        {

            return Ok();
        }


    }
}
