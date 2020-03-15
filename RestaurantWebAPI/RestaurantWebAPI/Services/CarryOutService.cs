using Microsoft.EntityFrameworkCore;
using RestaurantWebAPI.Data;
using RestaurantWebAPI.Models.Entities;
using RestaurantWebAPI.Models.ServiceRequests;
using RestaurantWebAPI.Models.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Services
{
    public class CarryOutService : ICarryOutService
    {
        private readonly ApplicationDbContext _context;

        public CarryOutService (ApplicationDbContext context)
        {
            _context = context;
        }

        public GetAllCarryOutsInCartResponse GetAllCarryOutsInCart(GetAllCarryOutsInCartRequest request)
        {
            var response = new GetAllCarryOutsInCartResponse
            {
                IsSuccessful = false,
                Message = ""
            };

            try
            {
                var customer = _context.Customers
                    .FirstOrDefault(x => x.Id == request.CustomerId);

                var carryOuts = _context.CarryOuts
                    .Include(x => x.Customer)
                    .Include(x => x.Food)
                    .Include(x => x.Beverage)
                    .Where(x => x.Customer.Id == customer.Id)
                    .Where(x => x.SubmissionTime == null)
                    .ToList();

                response.CarryOuts = carryOuts;
                response.IsSuccessful = true;
            }
            catch(Exception ex)
            {
                response.Message = ex.ToString();
            }

            return response;
        }

        public GetAllCarryOutsForDateResponse GetAllCarryOutsForDate(GetAllCarryOutsForDateRequest request)
        {
            var response = new GetAllCarryOutsForDateResponse
            {
                IsSuccessful = false,
                Message = ""
            };

            try
            {
                var carryOuts = _context.CarryOuts
                    .Include(x => x.Customer)
                    .Where(x => x.Customer.FirstName == request.Customer.FirstName)
                    .Where(x => x.Customer.LastName == request.Customer.LastName)
                    .Where(x => x.Customer.PhoneNumber == request.Customer.PhoneNumber)
                    //.Where(x => x.SubmissionTime.Date.ToString("MM/dd/yyyy") == request.Date.Date.ToString("MM/dd/yyyy"))
                    .ToList();

                response.CarryOuts = carryOuts;
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }

            return response;
        }

        public CreateCarryOutResponse CreateCarryOut(CreateCarryOutRequest request)
        {
            var response = new CreateCarryOutResponse
            {
                IsSuccessful = false,
                Message = ""
            };

            try
            {
                var customer = _context.Customers
                    .Where(x => x.Id == request.Customer.Id)
                    .Where(x => x.FirstName == request.Customer.FirstName)
                    .Where(x => x.LastName == request.Customer.LastName)
                    .FirstOrDefault();

                var maxBundleId = _context.CarryOuts
                    .Max(x => x.BundleId);

                maxBundleId++;
                    
                if (customer != null)
                {
                    var carryOutItemsToSubmit = _context.CarryOuts
                        .Include(x => x.Customer)
                        .Where(x => x.Customer == customer)
                        .Where(x => x.SubmissionTime == null)
                        .ToList();

                    _context.UpdateRange(carryOutItemsToSubmit);

                    foreach (var item in carryOutItemsToSubmit)
                    {
                        item.SubmissionTime = DateTime.Now;
                        item.BundleId = maxBundleId;
                    }

                    _context.SaveChanges();
                }

                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }

            return response;
        }
        public UpdateCarryOutResponse UpdateCarryOut(UpdateCarryOutRequest request)
        {
            var response = new UpdateCarryOutResponse
            {
                IsSuccessful = false,
                Message = ""

            };

            try
            {
                // Might break, felt cute, might delete later??
                var carryOutToUpdate = _context.CarryOuts
                    .Include(x => x.Customer)
                    .Where(x => x.BundleId == request.CarryOutToUpdate[0].BundleId)
                    .ToList();

                // Update each carry out with the beverage/food in the request.
                for (int i = 0; i < carryOutToUpdate.Count; i++)
                {
                    carryOutToUpdate[i].Beverage = request.CarryOutToUpdate[i].Beverage;
                    carryOutToUpdate[i].BeverageQuantity = request.CarryOutToUpdate[i].BeverageQuantity;
                    carryOutToUpdate[i].Food = request.CarryOutToUpdate[i].Food;
                    carryOutToUpdate[i].FoodQuantity = request.CarryOutToUpdate[i].FoodQuantity;
                }

                _context.UpdateRange(carryOutToUpdate);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Message = ex.ToString();
            }

            return response;
        }

        public AddToCartResponse AddToCart(AddToCartRequest request)
        {
            var response = new AddToCartResponse
            {
                IsSuccessful = false,
                Message = ""
            };

            try
            {
                var customer = _context.Customers
                    .Where(x => x.Id == request.CarryOutToAddToCart.CustomerId)
                    .FirstOrDefault();

                Food food = null;
                List<CarryOut> existingFood = null;
                Beverage beverage = null;
                List<CarryOut> existingBeverages = null;
                int currentFoodQuantity = 0;
                int currentBeverageQuantity = 0;

                if (request.CarryOutToAddToCart.Food != null)
                {
                    food = _context.Foods
                    .Where(x => x.Id == request.CarryOutToAddToCart.Food.Id)
                    .FirstOrDefault();
                }
                if (request.CarryOutToAddToCart.Beverage != null)
                {
                    beverage = _context.Beverages
                        .Where(x => x.Id == request.CarryOutToAddToCart.Beverage.Id)
                        .FirstOrDefault();
                }

                if (customer != null)
                {
                    if (food != null)
                    {
                        existingFood = _context.CarryOuts
                            .Include(x => x.Food)
                            .Include(x => x.Beverage)
                            .Include(x => x.Customer)
                            .Where(x => x.Customer.Id == customer.Id)
                            .Where(x => x.Food.Name == food.Name)
                            .Where(x => x.SubmissionTime == null)
                            .ToList();

                        foreach (var item in existingFood)
                        {
                            currentFoodQuantity += item.FoodQuantity;
                        }

                        _context.RemoveRange(existingFood);
                    }

                    if (beverage != null)
                    {
                        existingBeverages = _context.CarryOuts
                            .Include(x => x.Food)
                            .Include(x => x.Beverage)
                            .Include(x => x.Customer)
                            .Where(x => x.Customer.Id == customer.Id)
                            .Where(x => x.Beverage.Name == beverage.Name)
                            .Where(x => x.SubmissionTime == null)
                            .ToList();

                        foreach (var item in existingBeverages)
                        {
                            currentBeverageQuantity += item.BeverageQuantity;
                        }

                        _context.RemoveRange(existingBeverages);
                    }

                    var cartItem = new CarryOut
                    {
                        Beverage = beverage,
                        BeverageQuantity = request.CarryOutToAddToCart.BeverageQuantity + currentBeverageQuantity,
                        BundleId = 0,
                        Customer = customer,
                        Food = food,
                        FoodQuantity = request.CarryOutToAddToCart.FoodQuantity + currentFoodQuantity,
                        SubmissionTime = null,
                        Id = 0
                    };

                    _context.Add(cartItem);
                    _context.SaveChanges();

                    response.IsSuccessful = true;
                    response.Message = "Successfully added carry out to cart for the customer.";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }

            return response;
        }

        public RemoveFromCartResponse RemoveFromCart(RemoveFromCartRequest request)
        {
            var response = new RemoveFromCartResponse
            {
                IsSuccessful = false,
                Message = ""
            };

            try
            {
                var carryOutItemToDelete = _context.CarryOuts
                    .Include(x => x.Customer)
                    .Where(x => x.Customer.Id == request.CustomerId)
                    .Where(x => x.Id == request.CarryOutId)
                    .FirstOrDefault();
                
                if (carryOutItemToDelete != null)
                {
                    _context.Remove(carryOutItemToDelete);
                    _context.SaveChanges();

                    response.IsSuccessful = true;
                    response.Message = $"Successfully deleted Carry Out Item Record with Id: ${request.CarryOutId}.";
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Message = $"Was not able to delete Carry Out Item Record with Id: ${request.CarryOutId}.";
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
