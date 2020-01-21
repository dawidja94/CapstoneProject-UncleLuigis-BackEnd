using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Models.Entities
{
    public class CarryOut
    {
        [Key]
        public int Id { get; set; }
       public int BundleId { get; set; }
        public int CustomerId { get; set; }
        [Required]
        public int FoodId { get; set; }
        [Required]
        public int FoodQuanity { get; set; }
        public int BeverageID { get; set; }
        [Required]
        public int BeverageQuantity { get; set; }
        public DateTime SubmissionTime { get; set; }



    }
}
