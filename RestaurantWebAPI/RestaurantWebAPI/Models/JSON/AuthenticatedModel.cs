using RestaurantWebAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Models.JSON
{
    public class AuthenticatedModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public Customer Customer { get; set; }
    }
}
