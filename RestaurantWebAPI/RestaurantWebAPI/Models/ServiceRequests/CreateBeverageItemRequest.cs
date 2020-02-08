using RestaurantWebAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Models.ServiceRequests
{
    public class CreateBeverageItemRequest : BaseServiceRequest
    {
        public Beverage BeverageItem { get; set; }
    }
}
