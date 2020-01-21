using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using RestaurantWebAPI.Data;
using RestaurantWebAPI.Models.Entities;

namespace RestaurantWebAPI.Services
{
    public class FoodService : IFoodService
    {
        private readonly ApplicationDbContext _context;

        public FoodService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Food> GetAllFoodMenuItems()
        {
            var foodItems = _context.Foods.ToList();
            return foodItems;
        }
    }
}
