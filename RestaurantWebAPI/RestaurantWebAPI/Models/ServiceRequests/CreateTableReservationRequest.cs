using RestaurantWebAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Models.ServiceRequests
{
    public class CreateTableReservationRequest : BaseServiceRequest
    {
        public int TableId { get; set; }
        public int CustomerId { get; set; }
        public int PartySize { get; set; }
    }
}
