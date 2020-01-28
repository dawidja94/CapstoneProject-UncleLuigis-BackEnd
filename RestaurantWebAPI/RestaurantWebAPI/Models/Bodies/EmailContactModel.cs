using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Models.Bodies
{
    public class EmailContactModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
    }
}
