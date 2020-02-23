using RestaurantWebAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Models.ServiceResponses
{
    public class CreateCustomerResponse : BaseServiceResponse
    {
        public Customer CreatedCustomer { get; set; }
    }
}
