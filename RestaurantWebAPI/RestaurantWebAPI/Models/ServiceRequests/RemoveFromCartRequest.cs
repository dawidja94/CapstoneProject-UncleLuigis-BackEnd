using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Models.ServiceRequests
{
    public class RemoveFromCartRequest : BaseServiceRequest
    {
        public int CarryOutId { get; set; }
        public int CustomerId { get; set; }
    }
}
