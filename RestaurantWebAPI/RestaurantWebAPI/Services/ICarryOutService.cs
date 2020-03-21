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
        GetAllCarryOutsForCustomerResponse GetAllCarryOutsForCustomer(GetAllCarryOutsForCustomerRequest request);
        GetAllCarryOutsForDateResponse GetAllCarryOutsForDate(GetAllCarryOutsForDateRequest request);
        GetCarryOutByIdResponse GetCarryOutById(GetCarryOutByIdRequest request);
        CreateCarryOutResponse CreateCarryOut(CreateCarryOutRequest request);
        UpdateCarryOutResponse UpdateCarryOut(UpdateCarryOutRequest request);
        AddToCartResponse AddToCart(AddToCartRequest request);
        RemoveFromCartResponse RemoveFromCart(RemoveFromCartRequest request);
    }
}
