
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Models.ServiceRequests
{
    public class DeleteFoodItemRequest :  BaseServiceRequest
    {
        public int Id { get; set; }
    }
}
