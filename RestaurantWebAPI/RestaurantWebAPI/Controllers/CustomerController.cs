using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantWebAPI.Models.Bodies;
using RestaurantWebAPI.Models.Entities;
using RestaurantWebAPI.Models.ServiceRequests;
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

        // POST: Customer/GetCustomers
        [HttpPost("GetCustomer")]
        public IActionResult GetCustomers(CustomerModel body)
        {
            var request = new GetCustomerRequest
            {
                Body = body
            };

            var response = _customerService.GetCustomer(request);

            if (response.IsSuccessful)
            {
                return Ok(response.Customer);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        //POST : Customer/CreateCustomer
        [HttpPost("CreateCustomer")]
        public IActionResult CreateCustomer(Customer body)
        {
            var request = new CreateCustomerRequest
            {
                Customer = body
            };

            var response = _customerService.CreateCustomer(request);

            if (response.IsSuccessful)
            {
                return Ok();
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        // PUT: Customer/UpdateByCustomer
        [HttpPut("UpdateByCustomer")]
        public IActionResult UpdateCustomerInfoByCustomer(Customer body)
        {
            var request = new UpdateCustomerRequest
            {
                CustomerToUpdate = body
            };

            var response =_customerService.UpdateCustomerByCustomer(request);

            if (response.IsSuccessful)
            {
                return Ok();
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        // PUT: Customer/UpdateByAdmin
        [HttpPut("UpdateByAdmin")]
        public IActionResult UpdateCustomerInfoByAdmin(Customer body)
        {
            var request = new UpdateCustomerRequest
            {
                CustomerToUpdate = body
            };

            var response = _customerService.UpdateCustomerAdministrative(request);

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
