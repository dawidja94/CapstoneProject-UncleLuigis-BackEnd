using Microsoft.EntityFrameworkCore;
using RestaurantWebAPI.Data;
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

        public GetAllCarryOutsResponse GetAllCarryOuts(GetAllCarryOutsRequest request)
        {
            var response = new GetAllCarryOutsResponse
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
                    .Where(x => x.SubmissionTime.Date.ToString("MM/dd/yyyy") == request.Date.Date.ToString("MM/dd/yyyy"))
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
                var maxID = _context.CarryOuts.Max(x => x.BundleId);

                if (maxID != 0)
                {
                    maxID++;
                }

                foreach (var item in request.CarryOut)
                {
                    item.BundleId = maxID;
                }

                _context.AddRange(request.CarryOut);
                _context.SaveChanges();

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
    }
}
