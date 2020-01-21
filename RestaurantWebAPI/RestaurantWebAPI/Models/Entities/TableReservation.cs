using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Models.Entities
{
    public class TableReservation
    {
        [Key]
        public int Id { get; set; }
        public Customer Customer { get; set; }
        [Required]
        public int PartySize { get; set; }
        [Required]
        public int TableSize { get; set; }
        [Required]
        public string ReservationTable {get; set; }
        [Required]
        public string ReservationDate { get; set; }

    }

}
