using RestaurantWebAPI.Models.ServiceRequests;
using RestaurantWebAPI.Models.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Services
{
    public interface ICarryOutService
    {
        GetAllCarryOutsInCartResponse GetAllCarryOutsInCart(GetAllCarryOutsInCartRequest request);
        GetAllCarryOutsForDateResponse GetAllCarryOutsForDate(GetAllCarryOutsForDateRequest request);
        CreateCarryOutResponse CreateCarryOut(CreateCarryOutRequest request);
        UpdateCarryOutResponse UpdateCarryOut(UpdateCarryOutRequest request);
        AddToCartResponse AddToCart(AddToCartRequest request);
        RemoveFromCartResponse RemoveFromCart(RemoveFromCartRequest request);
    }
}
