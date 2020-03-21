using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Models.ServiceRequests
{
    public class GetAvailableTableReservationsRequest : BaseServiceRequest
    {
        public int PartySize { get; set; }
        public string ReservationDate { get; set; }
        public string TimeSlot { get; set; }
    }
}
