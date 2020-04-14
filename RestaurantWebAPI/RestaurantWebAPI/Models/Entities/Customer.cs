using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Models.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public DateTime? MemberSince { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
