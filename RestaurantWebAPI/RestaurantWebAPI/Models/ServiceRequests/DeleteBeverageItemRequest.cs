using RestaurantWebAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Models.ServiceRequests
{
    public class DeleteBeverageItemRequest : BaseServiceRequest
    {
        public int Id { get; set; }
    }
}
