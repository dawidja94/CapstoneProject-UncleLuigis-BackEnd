using RestaurantWebAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Models.Bodies
{
    public class CarryOutBody
    {
        public int Id { get; set; }
        public int BundleId { get; set; }
        public int CustomerId { get; set; }
        public Food Food { get; set; }
        public int FoodQuantity { get; set; }
        public Beverage Beverage { get; set; }
        public int BeverageQuantity { get; set; }
        public DateTime? SubmissionTime { get; set; }
    }
}
