using RestaurantWebAPI.Models.Bodies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Models.ServiceRequests
{
    public class GetAllCarryOutsForDateRequest : BaseServiceRequest
    {
        public CustomerModel Customer { get; set; }
        public DateTime Date { get; set; }
    }
}
