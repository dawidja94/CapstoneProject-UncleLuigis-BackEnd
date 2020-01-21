using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using RestaurantWebAPI.Models.Entities;

namespace RestaurantWebAPI.Services
{
    public interface IFoodService
    {
        List<Food> GetAllFoodMenuItems();
    }
}
