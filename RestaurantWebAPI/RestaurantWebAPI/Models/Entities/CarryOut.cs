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
        
        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        
        [ForeignKey("FoodId")]
        public Food FoodId { get; set; }
        [Required]
        public int FoodQuanity { get; set; }
        [ForeignKey("BeverageID")]
        public Beverage BeverageID { get; set; }
        [Required]
        public int BeverageQuantity { get; set; }
        public DateTime SubmissionTime { get; set; }



    }
}
