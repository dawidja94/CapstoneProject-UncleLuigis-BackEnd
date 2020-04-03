using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Models.Bodies
{
    public class ReserveTableBody
    {
        public int CustomerId { get; set; }
        public int TableId { get; set; }
        public int PartySize { get; set; }
    }
}
