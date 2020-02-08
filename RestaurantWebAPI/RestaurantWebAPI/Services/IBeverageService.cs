using RestaurantWebAPI.Models.ServiceRequests;
using RestaurantWebAPI.Models.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Services
{
    public interface IBeverageService
    {
        GetAllBeverageItemsResponse GetAllBeverageItems(GetAllBeverageItemsRequest request);
        CreateBeverageItemResponse CreateBeverageItem(CreateBeverageItemRequest request);
        UpdateBeverageItemResponse UpdateBeverageItem(UpdateBeverageItemRequest request);
        DeleteBeverageItemResponse DeleteBeverageItem(DeleteBeverageItemRequest request);
    }
}
