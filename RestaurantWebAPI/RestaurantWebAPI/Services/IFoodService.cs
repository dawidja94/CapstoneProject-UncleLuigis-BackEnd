using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using RestaurantWebAPI.Models.Entities;
using RestaurantWebAPI.Models.ServiceRequests;
using RestaurantWebAPI.Models.ServiceResponses;

namespace RestaurantWebAPI.Services
{
    public interface IFoodService
    {
        GetAllFoodMenuItemsResponse GetAllFoodMenuItems(GetAllFoodMenuItemsRequest request);
        CreateFoodItemResponse CreateFoodItem(CreateFoodItemRequest request);
        UpdateFoodItemResponse UpdateFoodItem(UpdateFoodItemRequest request);
        DeleteFoodItemResponse DeleteFoodItem(DeleteFoodItemRequest request);
    }
}
