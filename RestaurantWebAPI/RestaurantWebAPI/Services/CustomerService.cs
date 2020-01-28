using RestaurantWebAPI.Data;
using RestaurantWebAPI.Models.ServiceRequests;
using RestaurantWebAPI.Models.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }
        public CreateCustomerResponse CreateCustomer(CreateCustomerRequest request)
        {
            var response = new CreateCustomerResponse
            {
                IsSuccessful = false,
                Message = ""
            };

            try
            {
                _context.Add(request.Customer);
                _context.SaveChanges();

                response.IsSuccessful = true;
            }
            catch(Exception ex)
            {
                response.Message = ex.ToString();
            }
            return response;
        }

        public UpdateCustomerResponse UpdateCustomerAdministrative(UpdateCustomerRequest request)
        {
            var response = new UpdateCustomerResponse
            {
                IsSuccessful = false,
                Message = ""
            };

            try
            {
                var customerToUpdate = _context.Customers
                        .Where(x => x.Id == request.CustomerToUpdate.Id)
                        .Where(x => x.FirstName == request.CustomerToUpdate.FirstName)
                        .Where(x => x.LastName == request.CustomerToUpdate.LastName)
                        .Where(x => x.PhoneNumber == request.CustomerToUpdate.PhoneNumber)
                        .FirstOrDefault();


                if (customerToUpdate != null)
                {
                    customerToUpdate.FirstName = request.CustomerToUpdate.FirstName;
                    customerToUpdate.LastName = request.CustomerToUpdate.LastName;
                    customerToUpdate.PhoneNumber = request.CustomerToUpdate.PhoneNumber;
                    customerToUpdate.MemberSince = request.CustomerToUpdate.MemberSince;
                    customerToUpdate.DateOfBirth = request.CustomerToUpdate.DateOfBirth;
                    customerToUpdate.Email = request.CustomerToUpdate.Email;

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
        public UpdateCustomerResponse UpdateCustomerByCustomer(UpdateCustomerRequest request)
        {
            var response = new UpdateCustomerResponse
            {
                IsSuccessful = false,
                Message = ""
            };

            try
            {
                var customerToUpdate = _context.Customers
                        .Where(x => x.FirstName == request.CustomerToUpdate.FirstName)
                        .Where(x => x.LastName == request.CustomerToUpdate.LastName)
                        .Where(x => x.PhoneNumber == request.CustomerToUpdate.PhoneNumber)
                        .FirstOrDefault();

                if (customerToUpdate != null)
                {
                    customerToUpdate.Email = request.CustomerToUpdate.Email;
                    customerToUpdate.PhoneNumber = request.CustomerToUpdate.PhoneNumber;
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
