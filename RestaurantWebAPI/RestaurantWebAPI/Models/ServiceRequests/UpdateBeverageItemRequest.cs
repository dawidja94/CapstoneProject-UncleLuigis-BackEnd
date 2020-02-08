using RestaurantWebAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Models.ServiceRequests
{
    public class UpdateBeverageItemRequest : BaseServiceRequest
    {
       public Beverage BeverageItemToUpdate { get; set; }
    }
}
