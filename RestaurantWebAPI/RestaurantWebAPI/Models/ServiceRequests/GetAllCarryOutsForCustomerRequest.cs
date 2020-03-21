using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Models.ServiceRequests
{
    public class GetAllCarryOutsForCustomerRequest : BaseServiceRequest
    {
        public int CustomerId { get; set; }
        public int BundleId { get; set; }
    }
}
