using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Models.Entities
{
    public class CarryOut
    {
        [Key]
        public int Id { get; set; }
        public int BundleId { get; set; }
        public Customer Customer { get; set; }
        public Food Food { get; set; }
        [Required]
        public int FoodQuantity { get; set; }
        public Beverage Beverage { get; set; }
        [Required]
        public int BeverageQuantity { get; set; }
        public DateTime SubmissionTime { get; set; }
    }
}
