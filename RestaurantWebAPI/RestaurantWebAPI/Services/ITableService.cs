using RestaurantWebAPI.Models.ServiceRequests;
using RestaurantWebAPI.Models.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Services
{
    public interface ITableService
    {
        GetAvailableTableReservationsResponse GetAvailableTableReservations(GetAvailableTableReservationsRequest request);
        GetTableReservationsByCustomerResponse GetTableReservationsByCustomer(GetTableReservationsByCustomerRequest request);
        CreateTableReservationResponse CreateTableReservation(CreateTableReservationRequest request);
        UpdateTableReservationResponse UpdateTableReservation(UpdateTableReservationRequest request);
    }
}
