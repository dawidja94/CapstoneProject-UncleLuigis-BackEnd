using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public Customer Customer { get; set; }
        public bool IsAdmin { get; set; }
    }
}
