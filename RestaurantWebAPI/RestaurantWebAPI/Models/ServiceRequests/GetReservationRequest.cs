﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Models.ServiceRequests
{
    public class GetReservationRequest
    {
        public int ReservationId { get; set; }
        public int CustomerId { get; set; }
    }
}
