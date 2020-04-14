﻿using Microsoft.EntityFrameworkCore;
using RestaurantWebAPI.Data;
using RestaurantWebAPI.Models.Bodies;
using RestaurantWebAPI.Models.Entities;
using RestaurantWebAPI.Models.ServiceRequests;
using RestaurantWebAPI.Models.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Services
{
    public class CarryOutService : ICarryOutService
    {
        private readonly ApplicationDbContext _context;

        public CarryOutService(ApplicationDbContext context)
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
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }

            return response;
        }

        public GetAllCarryOutsForCustomerResponse GetAllCarryOutsForCustomer(GetAllCarryOutsForCustomerRequest request)
        {
            var response = new GetAllCarryOutsForCustomerResponse
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
                .Where(x => x.Customer.Id == customer.Id)
                .Where(x => x.SubmissionTime != null)
                .Where(x => x.BundleId != 0)
                .ToList();

                var distinctRecords = carryOuts
                   .GroupBy(x => x.BundleId)
                   .Select(x => x.First())
                   .OrderByDescending(x => x.BundleId)
                   .ToList();
          
                carryOuts = distinctRecords;
                response.CarryOuts = carryOuts;
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }
            return response;
        }
        public GetCarryOutByIdResponse GetCarryOutById(GetCarryOutByIdRequest request)
        {

            var response = new GetCarryOutByIdResponse
            {
                IsSuccessful = false,
            };

            try
            {
                var bundleId = _context.CarryOuts
               .FirstOrDefault(x => x.Id == request.BundleId);

                var carryOuts = _context.CarryOuts
                    .Include(x => x.Food)
                    .Include(x => x.Beverage)
                    .Include(x => x.Customer)
                    .Where(x => x.BundleId == request.BundleId)
                    .Where(x => x.SubmissionTime != null)
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
                var carryOutToUpdate = _context.CarryOuts
                    .Include(x => x.Customer)
                    .Where(x => x.Id == request.Id)
                    .Where(x => x.Customer.Id == request.CustomerId)
                    .FirstOrDefault();

                _context.Update(carryOutToUpdate);

                if (request.FoodQuantity != -99)
                {
                    carryOutToUpdate.FoodQuantity = request.FoodQuantity;

                }
                else if (request.BeverageQuantity != -99)
                {
                    carryOutToUpdate.BeverageQuantity = request.BeverageQuantity;

                }

                if (request.FoodQuantity == 0 || request.BeverageQuantity == 0)
                {
                    _context.Remove(carryOutToUpdate);
                }

                _context.SaveChanges();

                response.IsSuccessful = true;
                response.Message = "Successfully updated Carry Out item.";
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
