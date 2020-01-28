using RestaurantWebAPI.Models.Entities;
using RestaurantWebAPI.Models.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Models.ServiceResponses
{
    public class GenerateJWTResponse : BaseServiceResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public AuthenticatedModel AuthenticatedModel { get; set; }
        public string Username { get; set; }
    }
}
