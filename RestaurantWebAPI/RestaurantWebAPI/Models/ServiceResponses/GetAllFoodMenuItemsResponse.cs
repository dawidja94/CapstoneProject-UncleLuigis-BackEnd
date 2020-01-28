using RestaurantWebAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Models.ServiceResponses
{
    public class GetAllFoodMenuItemsResponse : BaseServiceResponse
    {
        public List<Food> FoodItems { get; set; }
    }
}
