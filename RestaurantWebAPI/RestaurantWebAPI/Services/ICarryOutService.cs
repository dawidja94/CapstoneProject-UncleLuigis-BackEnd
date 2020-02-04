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
        GetAllCarryOutsResponse GetAllCarryOuts(GetAllCarryOutsRequest request);
        GetAllCarryOutsForDateResponse GetAllCarryOutsForDate(GetAllCarryOutsForDateRequest request);
        CreateCarryOutResponse CreateCarryOut(CreateCarryOutRequest request);
        UpdateCarryOutResponse UpdateCarryOut(UpdateCarryOutRequest request);
    }
}
