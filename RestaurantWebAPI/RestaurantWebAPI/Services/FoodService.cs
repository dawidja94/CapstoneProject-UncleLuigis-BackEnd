using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using RestaurantWebAPI.Data;
using RestaurantWebAPI.Models.Entities;
using RestaurantWebAPI.Models.ServiceRequests;
using RestaurantWebAPI.Models.ServiceResponses;

namespace RestaurantWebAPI.Services
{
    public class FoodService : IFoodService
    {
        private readonly ApplicationDbContext _context;

        public FoodService(ApplicationDbContext context)
        {
            _context = context;
        }

        public CreateFoodItemResponse CreateFoodItem(CreateFoodItemRequest request)
        {
            var response = new CreateFoodItemResponse
            {
                IsSuccessful = false,
                Message = ""
            };

            try
            {
                // Create new Food record.
                // Make sure the Id (Primary Key) is set to 0 or not set at all for new items.
                _context.Add(request.FoodItem);

                // You need to use .SaveChanges() always after a POST, PUT, DELETE.
                _context.SaveChanges();

                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }

            return response;
        }

        public DeleteFoodItemResponse DeleteFoodItem(DeleteFoodItemRequest request)
        {
            var response = new DeleteFoodItemResponse
            {
                IsSuccessful = false,
                Message = ""
            };

            try
            {
                // Query and get the record where the ID matches via LINQ Where clause.
                var itemToDelete = _context.Foods
                    .Where(x => x.Id == request.Id)
                    .FirstOrDefault();

                if (itemToDelete != null)
                {
                    // Remove by tracking the record, and call SaveChanges() to physically delete the record.
                    _context.Remove(itemToDelete);
                    _context.SaveChanges();

                    response.IsSuccessful = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }

            return response;
        }

        public GetAllFoodMenuItemsResponse GetAllFoodMenuItems(GetAllFoodMenuItemsRequest request)
        {
            var response = new GetAllFoodMenuItemsResponse
            {
                IsSuccessful = false,
                Message = ""
            };

            try
            {
                // Query our Foods table in SQL Server. Convert to a List<Food>.
                var items = _context.Foods.ToList();

                // Handle null value conditionally.
                if (items != null)
                {
                    response.IsSuccessful = true;
                    response.FoodItems = items;
                }
                else
                {
                    response.Message = "Query returned null result.";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }

            return response;
        }

        public UpdateFoodItemResponse UpdateFoodItem(UpdateFoodItemRequest request)
        {
            var response = new UpdateFoodItemResponse
            {
                IsSuccessful = false,
                Message = ""
            };

            try
            {
                // Get the record to update by its Id.
                var itemToUpdate = _context.Foods
                    .Where(x => x.Id == request.FoodToUpdate.Id)
                    .FirstOrDefault();

                if (itemToUpdate != null)
                {
                    // Update the record.
                    itemToUpdate.Description = request.FoodToUpdate.Description;
                    itemToUpdate.GlutenFree = request.FoodToUpdate.GlutenFree;
                    itemToUpdate.Name = request.FoodToUpdate.Name;
                    itemToUpdate.Price = request.FoodToUpdate.Price;
                    itemToUpdate.Vegan = request.FoodToUpdate.Vegan;

                    // Save the changes.
                    _context.SaveChanges();

                    response.IsSuccessful = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }

            return response;
        }
    }
}
