using RestaurantWebAPI.Models.Bodies;
using RestaurantWebAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Models.ServiceRequests
{
    public class CreateCarryOutRequest : BaseServiceRequest
    {
        public CustomerBody Customer { get; set; }
    }
}
