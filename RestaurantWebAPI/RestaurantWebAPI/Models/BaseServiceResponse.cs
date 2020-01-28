using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Models
{
    public class BaseServiceResponse
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }
}
