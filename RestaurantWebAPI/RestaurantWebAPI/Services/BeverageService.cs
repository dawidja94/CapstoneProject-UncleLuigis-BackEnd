using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantWebAPI.Data;
using RestaurantWebAPI.Models.ServiceRequests;
using RestaurantWebAPI.Models.ServiceResponses;

namespace RestaurantWebAPI.Services
{
    public class BeverageService : IBeverageService
    {
        private readonly ApplicationDbContext _context;

        public BeverageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public GetAllBeverageItemsResponse GetAllBeverageItems(GetAllBeverageItemsRequest request)
        {
            var response = new GetAllBeverageItemsResponse
            {
                IsSuccessful = false,
                Message = ""
            };

            try
            {
                // Query our Foods table in SQL Server. Convert to a List<Food>.
                var items = _context.Beverages.ToList();

                // Handle null value conditionally.
                if (items != null)
                {
                    response.IsSuccessful = true;
                    response.BeverageItems = items;
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

        public CreateBeverageItemResponse CreateBeverageItem(CreateBeverageItemRequest request)
        {
            var response = new CreateBeverageItemResponse
            {
                IsSuccessful = false,
                Message = ""
            };

            try
            {
                _context.Add(request.BeverageItem);
                _context.SaveChanges();
                response.IsSuccessful = true;
            }
            catch(Exception ex)
            {
                response.Message = ex.ToString();

            }

            return response;
        }

        public UpdateBeverageItemResponse UpdateBeverageItem(UpdateBeverageItemRequest request)
        {
            var response = new UpdateBeverageItemResponse
            {
                IsSuccessful = false,
                Message = ""
            };

            try
            {
                var itemToUpdate = _context.Beverages
                    .Where(x => x.Id == request.BeverageItemToUpdate.Id)
                    .FirstOrDefault();

                if (itemToUpdate != null)
                {
                    itemToUpdate.Name = request.BeverageItemToUpdate.Name;
                    itemToUpdate.Price = request.BeverageItemToUpdate.Price;
                    itemToUpdate.Description = request.BeverageItemToUpdate.Description;
                    itemToUpdate.Size = request.BeverageItemToUpdate.Size;

                    _context.SaveChanges();

                    response.IsSuccessful = true;
                }
            }
            catch(Exception ex)
            {
                response.Message = ex.ToString();
            }
            return response;
        }

        public DeleteBeverageItemResponse DeleteBeverageItem(DeleteBeverageItemRequest request)
        {
            var response = new DeleteBeverageItemResponse
            {
                IsSuccessful = false,
                Message = ""
            };

            try
            {
                var itemToDelete = _context.Beverages
                     .Where(x => x.Id == request.Id)
                     .FirstOrDefault();
                if (itemToDelete != null)
                {
                    _context.Remove(itemToDelete);
                    _context.SaveChanges();

                    response.IsSuccessful = true;
                }
            }
            catch(Exception ex)
            {
                response.Message = ex.ToString();
            }
            return response;
        }
    }
}
