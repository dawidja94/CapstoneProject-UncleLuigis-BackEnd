using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Models.Bodies
{
    public class RemoveCarryOutBody
    {
        public int CustomerId { get; set; }
        public int CarryOutId { get; set; }
    }
}
