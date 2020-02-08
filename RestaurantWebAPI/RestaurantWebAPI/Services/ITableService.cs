using RestaurantWebAPI.Models.ServiceRequests;
using RestaurantWebAPI.Models.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Services
{
    interface ITableService
    {
        GetTableReservationsResponse GetTableReservations(GetTableReservationsRequest request);
        GetTableReservationsByCustomerResponse GetTableReservationsByCustomer(GetTableReservationsByCustomerRequest request);
        CreateTableReservationResponse CreateTableReservation(CreateTableReservationRequest request);
        UpdateTableReservationResponse UpdateTableReservation(UpdateTableReservationRequest request);
        AllocateNewTablesResponse AllocateNewTables(AllocateNewTablesRequest request);
    }
}
